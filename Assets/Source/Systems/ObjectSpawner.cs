using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 1f;
    public UnityEngine.Events.UnityEvent spawnedObject;

    public GameObject SpawnedObject
    {
        get { return _spawnedObject; }
    }

    private GameObject _spawnedObject;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= interval)
        {
            _timer = 0;
            _spawnedObject = Instantiate<GameObject>(prefab, transform.position, transform.rotation);

            spawnedObject.Invoke();
        }
    }
}
