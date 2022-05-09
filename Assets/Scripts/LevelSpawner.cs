using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

[SerializeField] private GameObject _currentStage;
[SerializeField] private GameObjectPool _stagePool;

private GameObject _nextStage;

    public void SpawnNextStage() {
        _nextStage = _stagePool.Get();
        _nextStage.GetComponent<Collider>().enabled = true;
        _nextStage.GetComponent<Stage>().ShuffleTraps();

        _nextStage.transform.position = _currentStage.transform.position + new Vector3(0, 0, 100);

        _currentStage = _nextStage;
    }
}
