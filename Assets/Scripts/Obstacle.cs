using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody _obstacleRb;
    private bool _hasCollided;

    private void Awake() {
        _obstacleRb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if(!_hasCollided) return;

        transform.Rotate(new Vector3(0, 20, 20) * Time.deltaTime);
    }

    private void ViolentlyThrow() {
        _hasCollided = true;
        _obstacleRb.constraints = RigidbodyConstraints.None;
        _obstacleRb.AddForce(Random.Range(-10,10), 15, 15, ForceMode.Impulse);
        print("Hit");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
            ViolentlyThrow();
    }
}
