using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnHit : MonoBehaviour
{
    public GameObject hitEffect;
    public LayerMask hitLayers;
    public GameObject destroyObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitLayers == (hitLayers | (1 << collision.gameObject.layer)))
        {
            Explode(transform.position, transform.rotation);

            DealDamage dealDamage = gameObject.GetComponent<DealDamage>();
            ITakeDamage takeDamage = collision.gameObject.GetComponent<ITakeDamage>();
            if (dealDamage != null && takeDamage != null)
            {
                takeDamage.SufferDamage(dealDamage.damage);
            }
        }
    }

    private void Explode(Vector3 position, Quaternion rotation)
    {
        if (hitEffect != null)
        {
            GameObject go = Instantiate<GameObject>(hitEffect, position, rotation);
        }

        Destroy(destroyObject);
    }
}
