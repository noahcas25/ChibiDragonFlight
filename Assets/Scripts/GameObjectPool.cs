using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
// Variables
    [SerializeField]
    private GameObject _prefab;

    public static GameObjectPool Instance { get; private set;}

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Awake() {
        Instance = this;
    }

// Dequeues GameObject from the pool
    public GameObject Get() {
        if(_pool.Count == 0) {
            AddToPool(1);
        }
        
        return _pool.Dequeue();
    }

// Enqueues GameObject back into pool
    public void ReturnToPool(GameObject objectReturning) {
        objectReturning.SetActive(false);
       _pool.Enqueue(objectReturning);
    }

// Creates new objects to add to the pool, enqueues
    public void AddToPool(int count) {
        for(int i = 0; i < count; i++) {
            GameObject newObject;
            newObject = Instantiate(_prefab.transform.GetChild(Random.Range(0, _prefab.transform.childCount)).gameObject);
            newObject.SetActive(false);

            _pool.Enqueue(newObject);
        }
    }
}
