using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dragon : MonoBehaviour
{
    [SerializeField] private GameManagerScriptableObject _gameManager;
    [SerializeField] private Transform _cam;

    [SerializeField] private Rigidbody _dragonRB;
    [SerializeField] private Animator _dragonAnimator;
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _jumpSensitivity = 10f;

    private bool _canJump = true;
    private bool _canMove = false;

    private void OnEnable() => _gameManager._gameStateEvent.AddListener(PlayerDied);

    private void OnDisable() => _gameManager._gameStateEvent.RemoveListener(PlayerDied);

    private void Update() {
        if(_canMove)
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

// Switch to public if seperate touch controller script
    private void Jump() {
        if(!_canJump) return;

        // re-examine this later
        _dragonAnimator.SetFloat("Running", 0);
        _dragonRB.velocity = new Vector3(0,0,0);
        StartCoroutine(JumpDelay());

        for(int i= 0; i < _jumpSensitivity; i++) {
            _dragonRB.AddForce(0, 1, 0, ForceMode.Impulse);
            // re-examine this later
            VelocityToRotation();
        }
    }

     private IEnumerator JumpDelay() {
        _canJump = false;
        yield return new WaitForSeconds(0.1f);
        _canJump = true;
    }

    private void PlayerDied(bool gameState) {
        if(gameState) {
            _canMove = true;
            return;
        }

        _canMove = false;
        _dragonRB.velocity = new Vector3(0,0,0);
        _dragonRB.AddForce(0, 4, 2, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Floor"))
            _dragonAnimator.SetFloat("Running", 1);

        if(other.CompareTag("Trap")) {
            _gameManager.ChangeGameState(false);
            transform.GetComponent<Collider>().enabled = false;
        }
        
        if(other.CompareTag("Hurdle")) {
            _gameManager.ChangeScore(1);
            other.GetComponent<Collider>().enabled = false;
        }
    }
}