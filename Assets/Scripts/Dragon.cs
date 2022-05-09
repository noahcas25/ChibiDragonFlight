using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dragon : MonoBehaviour
{
    [SerializeField] private Rigidbody _dragonRB;
    [SerializeField] private Animator _dragonAnimator;

    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _jumpSensitivity = 10f;
    [SerializeField] private Transform _cam;

    private bool _canJump = true;
    private bool _canMove = true;
    private bool _gameOver = false;


    private void Start() {
        Application.targetFrameRate = 60;
    }

    private void Update() {
        // if(_gameOver) return;
    
        Movement();
    }

    private void Movement() {
        VelocityToRotation();
        Keys();
        TouchControls();

        transform.position += new Vector3(0, 0, _walkSpeed) * Time.deltaTime;
        _cam.position  += new Vector3(0, 0, _walkSpeed) * Time.deltaTime;
    }

    private void Keys() {
        if(Input.GetKeyDown("w"))
            Jump();

        if(Input.GetKeyDown("r"))
            SceneManager.LoadScene("LastScene");
    }

    private void TouchControls() {
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) 
            Jump();
    }

    private void VelocityToRotation() {
        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = new Vector3(3 * -_dragonRB.velocity.y - 10, 0, 0);
        transform.rotation = newRotation;
    }

    public void Jump() {
        if(!_canJump) return;

        _dragonAnimator.SetFloat("Running", 0);
        _dragonRB.velocity = new Vector3(0,0,0);
        StartCoroutine(JumpDelay());

        for(int i= 0; i < _jumpSensitivity; i++) {
            _dragonRB.AddForce(0, 1, 0, ForceMode.Impulse);
            VelocityToRotation();
        }
    }

     private IEnumerator JumpDelay() {
        _canJump = false;
        yield return new WaitForSeconds(0.1f);
        _canJump = true;
    }

    private void PlayerDied() {
        _gameOver = true;
        _dragonRB.velocity = new Vector3(0,0,0);
        _dragonRB.AddForce(0, 4, 2, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Floor"))
            _dragonAnimator.SetFloat("Running", 1);

        if(other.CompareTag("Trap")) 
            PlayerDied();
    }
}