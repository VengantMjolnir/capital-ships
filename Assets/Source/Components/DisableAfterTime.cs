using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public float lifetime = 1f;
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
            gameObject.SetActive(false);
        }
    }
}
