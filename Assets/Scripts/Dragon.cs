using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dragon : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Rigidbody _dragonRB;
    [SerializeField] private Animator _dragonAnimator;

    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _jumpSensitivity = 10f;

    private bool _canJump = true;
    private bool _canMove = false;
    private Quaternion _newRotation = new Quaternion();

    private void OnEnable() {
        GameManager.Instance._gameStateEvent.AddListener(PlayerDied);

         if(PlayerPrefs.HasKey("skinMaterial"))
            ChangeSkinMaterial("T_Dragon_" + PlayerPrefs.GetInt("skinMaterial"));
    }

    private void OnDisable() => GameManager.Instance._gameStateEvent.RemoveListener(PlayerDied);

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

    private void VelocityToRotation() {
        _newRotation.eulerAngles = new Vector3(3 * -_dragonRB.velocity.y - 10, 0, 0);
        transform.rotation = _newRotation;
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

    private void Jump() {
        if(!_canJump) return;

        AudioManager.Instance.PlayOneShot(0);
        _dragonAnimator.SetFloat("Running", 0);
        _dragonRB.velocity = new Vector3(0,0,0);
        StartCoroutine(JumpDelay());

        _dragonRB.AddForce(0, _jumpSensitivity, 0, ForceMode.Impulse);
    }

     private IEnumerator JumpDelay() {
        _canJump = false;
        yield return new WaitForSeconds(0.1f);
        _canJump = true;
    }

    private void PlayerDied(bool gameState) {
        // when game starts
        if(gameState) {
            _canMove = true;
            _dragonRB.AddForce(0, 3, 0, ForceMode.Impulse);
            return;
        }

        // when game ends
        _canMove = false;
        _dragonRB.velocity = new Vector3(0,0,0);
        _dragonRB.AddForce(0, 4, 2, ForceMode.Impulse);
    }

    private void ChangeSkinMaterial(string skinMaterial) {
        for(int i = 1; i < 5; i++) {
            transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material = Resources.Load(skinMaterial, typeof(Material)) as Material;
        } 
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Trap")) {
            GameManager.Instance.ChangeGameState(false);
            AudioManager.Instance.PlayOneShot(2);
            transform.GetComponent<Collider>().enabled = false;
        }
        
        if(other.CompareTag("Hurdle")) {
            GameManager.Instance.ChangeScore(1);
            other.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Floor" && _canMove)
            _dragonAnimator.SetFloat("Running", 1);
    }
}