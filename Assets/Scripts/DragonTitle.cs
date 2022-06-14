using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTitle : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private Animator _dragonAnimator;
    [SerializeField] private Rigidbody _dragonRB;

    private bool _canJump = true;
    private Vector3 _startPosition;

    private void OnEnable() {
         _startPosition = transform.position;
    }

    private void Update()
    { 
          VelocityToRotation();
          Jump();
          transform.position += new Vector3(0, 0, _walkSpeed) * Time.deltaTime;
    }

     private void VelocityToRotation() {
        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = new Vector3(3 * -_dragonRB.velocity.y-10, 0, 0);
        transform.rotation = newRotation;
    }

    private void Jump() {
        if(!_canJump) return;
        StartCoroutine(JumpDelay());
        _dragonRB.velocity = new Vector3(0,0,0);

        for(int i=0; i < 8f; i++) {
            _dragonRB.AddForce(0, 1, 0, ForceMode.Impulse);
            VelocityToRotation();
        }
    }

     private IEnumerator JumpDelay() {
        _canJump = false;
        yield return new WaitForSeconds(.65f);
        _canJump = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Trap")) {
            
            _dragonRB.velocity = new Vector3(0,0,0);
            transform.position = _startPosition;
        }
  }
}
