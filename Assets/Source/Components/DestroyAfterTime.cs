using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifespan = 1f;
    public GameObject hitEffect;
    private float _timer = 0f;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > lifespan)
        {
            Die();
        }
    }

    public void Die()
    {
        if (hitEffect != null)
        {
            GameObject go = Instantiate<GameObject>(hitEffect, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
