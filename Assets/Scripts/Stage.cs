using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] GameObjectPool _stagePool;
    [SerializeField] LevelSpawner _levelSpawner;

    private GameObject _traps;
    private GameObject[] _trapArray;

    private void Awake() {
        _traps = transform.GetChild(0).gameObject;
        _trapArray = new GameObject[_traps.transform.childCount];
        ObjectToArr();
    }

    private void ObjectToArr() {
        for(int i = 0; i < _trapArray.Length; i++) {
            _trapArray[i] = _traps.transform.GetChild(i).gameObject;
        }
    }

    public void ShuffleTraps() {
        for(int i = 0; i < _trapArray.Length; i++) {
            _trapArray[i].transform.position = new Vector3(_trapArray[i].transform.position.x, Random.Range(-.5f, 3.5f), _trapArray[i].transform.position.z);
        }
    }

    private IEnumerator ReturnToPoolDelay() {
        yield return new WaitForSeconds(5f);
        _stagePool.ReturnToPool(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
       if(other.CompareTag("Player")) {
           print("hit");
        //    other.enabled = false;
           _levelSpawner.SpawnNextStage();
           StartCoroutine(ReturnToPoolDelay());
       }
    }
}