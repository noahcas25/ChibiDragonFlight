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

    public void OnEnable() {
        for(int i = 0; i < _trapArray.Length; i++) {
            _trapArray[i].GetComponent<Collider>().enabled = true;
        }
    }

    private void Start() => ShuffleTraps();

    private void ObjectToArr() {
        for(int i = 0; i < _trapArray.Length; i++) {
            _trapArray[i] = _traps.transform.GetChild(i).gameObject;
        }
    }

    private void ShuffleTraps() {
        for(int i = 0; i < _trapArray.Length; i++) {
            _trapArray[i].transform.localPosition = new Vector3(_trapArray[i].transform.localPosition.x, Random.Range(1f, 4.6f), _trapArray[i].transform.localPosition.z);
        }
    }

    private IEnumerator ReturnToPoolDelay() {
        yield return new WaitForSeconds(6f);
        _stagePool.ReturnToPool(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
       if(other.CompareTag("Player")) {
           GetComponent<Collider>().enabled = false;
           _levelSpawner.SpawnNextStage();
           StartCoroutine(ReturnToPoolDelay());
       }
    }
}