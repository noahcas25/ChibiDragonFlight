using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField] private Transform _dragonTransform;
    [SerializeField] private Rigidbody _dragonRB;
    [SerializeField] private Animator _dragonAnimator;

    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _horizontalSensitivity = 5f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _cam;


    private Touch _touch;
    private Vector3 _direction;
    private float _horizontal = 0f;
    private float _vertical = 0f;

    private bool _canJump = true;
    private bool _canFire = true;

    private void Start() {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _horizontal = _joystick.Horizontal;
        _vertical = _joystick.Vertical;

        Vector3 movement = new Vector3(_horizontal, 0, 0);

        _dragonTransform.position += new Vector3(_horizontal * _horizontalSensitivity, 0, _walkSpeed) * Time.fixedDeltaTime;
        _cam.position  += new Vector3(0, 0, _walkSpeed) * Time.fixedDeltaTime;
 
        float velocityX = Vector3.Dot(movement, transform.right);
        float velocityY = Vector3.Dot(movement, transform.up);

        _dragonAnimator.SetFloat("VelocityX", velocityX, 0.1f, Time.fixedDeltaTime);
        _dragonAnimator.SetFloat("VelocityY", velocityY, 0.1f, Time.fixedDeltaTime);
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
}