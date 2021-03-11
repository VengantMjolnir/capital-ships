using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour, ITakeDamage
{
    public Health health;
    public GameObject shieldHit;
    public FloatReference shields;

    public void SufferDamage(float damage)        
    {
        if (shields.Value > 0)
        {
            shields.Value -= damage;
        }
        else if (health != null)
        {
            health.SufferDamage(damage);
        }
    }

    public void Die()
    {
        this.enabled = false;
    }
}
