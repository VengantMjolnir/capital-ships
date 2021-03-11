using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterTime : MonoBehaviour
{
    public float lifetime = 1f;
    public GameObject targetObject;
    public MonoBehaviour targetComponent;
    public bool enable = true;
    private float _timer = 0f;

    private void OnEnable()
    {
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > lifetime)
        {
            if (targetObject)
            {
                targetObject.SetActive(enable);
            }
            if (targetComponent)
            {
                targetComponent.enabled = enable;
            }
            enabled = false;
        }
    }
}
