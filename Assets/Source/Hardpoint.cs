using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardpoint : MonoBehaviour
{
    public float limitAngle = 30f;
    public float range = 5f;
    public float fireInterval = 1f;
    public float fireVariance = 0.1f;
    public float bulletForce = 10f;
    public bool rotateToTargetDirection = true;
    public bool waitsForShotToEnd = false;
    public bool attachToHardpoint = false;
    public bool startReadyToFire = false;
    public GameObject muzzleFlash;
    public GameObject bullet;

    private GameObject _target;
    private GameObject _shot;
    private Vector2 _fireDirection;
    private bool _canFire = false;

    private float _nextFireTime = float.MaxValue;

    private void OnEnable()
    {
        _nextFireTime = Time.time + fireInterval;
        if (startReadyToFire)
        {
            _nextFireTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canFire)
        {
            return;
        }

        if (_nextFireTime < Time.time)
        {
            _shot = Fire();
            if (_shot != null && attachToHardpoint)
            {
                _shot.transform.SetParent(transform, true);
            }
            if (waitsForShotToEnd)
            {
                _nextFireTime = float.MaxValue;
            }
            else
            {
                ScheduleNextFireTime();
            }
        }

        if (_nextFireTime >= float.MaxValue && _shot == null)
        {
            ScheduleNextFireTime();
        }
    }

    public void ScheduleNextFireTime()
    {
        _nextFireTime = Time.time + fireInterval + (Random.Range(-1f, 1f) * fireVariance);
    }

    public GameObject Fire()
    {
        if (_target == null)
        {
            return null;
        }

        Vector3 targetPosition = _target.transform.position;
        Rigidbody2D targetRB = _target.GetComponent<Rigidbody2D>();
        if (targetRB != null)
        {
            targetPosition.x += targetRB.velocity.x * 0.5f;
            targetPosition.y += targetRB.velocity.y * 0.5f;
        }

        Vector3 directionToTarget = targetPosition - transform.position;
        Quaternion rotation;
        Vector3 fireDirection;
        if (rotateToTargetDirection)
        {
            rotation = Quaternion.LookRotation(transform.forward, directionToTarget);
            fireDirection = directionToTarget.normalized;
        }
        else
        {
            rotation = transform.rotation;
            fireDirection = transform.up;
        }
        GameObject go = Instantiate<GameObject>(bullet);
        go.transform.position = transform.position;
        go.transform.rotation = rotation;

        if (muzzleFlash != null)
        {
            muzzleFlash.transform.rotation = rotation;
            muzzleFlash.SetActive(true);
        }

        Debug.DrawRay(transform.position, fireDirection * 5, Color.red, 1f);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = fireDirection * bulletForce;
        }
        IHoldsTarget[] holdsTargetList = go.GetComponentsInChildren<IHoldsTarget>(true);
        foreach (IHoldsTarget holdsTarget in holdsTargetList)
        {
            if (holdsTarget != null)
            {
                holdsTarget.SetTarget(_target);
            }
        }

        return go;
    }

    public GameObject Target
    {
        set { _target = value; }
        get { return _target; }
    }

    public Vector3 FireDirection
    {
        set { _fireDirection = value; }
        get { return _fireDirection; }
    }

    public bool AbleToFire
    {
        set { _canFire = value; }
        get { return _canFire; }
    }

    public bool CanSeePoint(Vector3 position)
    {
        if (!enabled || !gameObject.activeSelf)
        {
            return false;
        }
        Transform t = transform;

        float distance = Vector3.Distance(t.position, position);
        if (distance > range)
        {
            return false;
        }
        Vector3 directionToTarget = position - t.position;
        float d = Vector3.Dot(t.up, directionToTarget.normalized);
        float c = Mathf.Sin((90 - (limitAngle / 2)) * Mathf.Deg2Rad);
        return d > c;
    }
}
