using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    // [SerializeField] private Transform _dragonTransform;
    [SerializeField] private Rigidbody _dragonRB;
    [SerializeField] private Animator _dragonAnimator;

    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _horizontalSensitivity = 5f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _cam;
    [SerializeField] private Camera _camera;


    private Touch _touch;
    private Vector3 _direction;
    private float _horizontal = 0f;
    private float _vertical = 0f;

    private bool _canJump = true;
    private bool _canFire = true;
    private bool _canMove = true;


    private void Start() {
        Application.targetFrameRate = 60;
    }

    private void Update() {
        // Swipe();
        Keys();
        TouchControls();
        transform.position += new Vector3(0, 0, _walkSpeed) * Time.deltaTime;
        _cam.position  += new Vector3(0, 0, _walkSpeed) * Time.deltaTime;

    }

    private void Move(string direction) {
        switch(direction) {
        case "Left": transform.position += new Vector3(-1.5f, 0, 0);
        break;
         case "Right": transform.position += new Vector3(1.5f, 0, 0);
        break;
         case "Up": _dragonRB.velocity = new Vector3(0,0,0);
                    _dragonRB.AddForce(0, 10, 0, ForceMode.Impulse);
        break;
        }
    }

    private void Keys() {
        if(Input.GetKeyDown("w") && _canJump)
            Move("Up");

        // if(Input.GetKeyDown("a") && _canMove)
        //     Move("Left");

        // if(Input.GetKeyDown("d") && _canMove)
        //     Move("Right");

    }

    private void TouchControls() {
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) && _canJump) 
            Move("Up");
    }

    public void Jump() {
        if(!_canJump) return;

        _dragonRB.AddForce(0, 10, 0, ForceMode.Impulse);
        StartCoroutine(JumpDelay());
    }

    public void Fire() {
        if(!_canFire) return;

        StartCoroutine(FireDelay());
    }


    private IEnumerator MoveDelay() {
        _canMove = false;
        yield return new WaitForSeconds(0.2f);
        _canMove = true;
    }

    private IEnumerator JumpDelay() {
        _canJump = false;
        yield return new WaitForSeconds(1f);
        _canJump = true;
    }

    private IEnumerator FireDelay() {
        _canFire = false;
        yield return new WaitForSeconds(1f);
        _canFire = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Trap")) return;

        print("hit");
        _canJump = false;
    }
}