using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public List<ObjectSpawner> spawners = new List<ObjectSpawner>();
    public List<GameObject> trackedObjects = new List<GameObject>();
    public GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = trackedObjects.Count - 1; i >=0; --i)
        {
            if (trackedObjects[i] == null)
            {
                trackedObjects.RemoveAt(i);
            }
        }

        if (trackedObjects.Count == 0)
        {
            foreach (ObjectSpawner spawner in spawners)
            {
                spawner.gameObject.SetActive(true);
            }
        }
    }

    public void AddTrackedObject(ObjectSpawner spawner)
    {
        trackedObjects.Add(spawner.SpawnedObject);

        if (indicator != null)
        {
            GameObject go = Instantiate<GameObject>(indicator);
            IndicatorSystem id = go.GetComponent<IndicatorSystem>();
            if (id != null)
            {
                id.target = spawner.SpawnedObject.transform;
            }
        }
    }
}
