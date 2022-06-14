using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

[SerializeField] private GameObject _currentStage;
[SerializeField] private GameObjectPool _stagePool;

private GameObject _nextStage;
    
    public void OnEnable() => _stagePool.AddToPool(10);

    public void SpawnNextStage() {
        _nextStage = _stagePool.Get();
        _nextStage.SetActive(true);

        _nextStage.transform.position = _currentStage.transform.position + new Vector3(0f, 0f, 50f);
        _nextStage.GetComponent<Collider>().enabled = true;
        _currentStage = _nextStage;
    }
}
