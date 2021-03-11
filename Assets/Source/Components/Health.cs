using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ITakeDamage
{
    public FloatReference health;
    public bool destroyOnDeath = true;
    public GameObject onDestroyEffect;
    public GameObject destructionTarget;

    public void SufferDamage(float damage)
    {
        float lastHealth = health;
        health.Value -= damage;
        if (health <= 0f && lastHealth > 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        if (onDestroyEffect != null)
        {
            GameObject go = Instantiate<GameObject>(onDestroyEffect, transform.position, transform.rotation);
        }

        if (destructionTarget != null)
        {
            Destroy(destructionTarget);
        }
        else if(destroyOnDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            enabled = false;
        }
    }
}
