using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public TargetingSystem targetingSystem;
    public float maxTargetRange = 10f;
    public float minTargetRange = 4f;
    public float idleTime = 3f;
    public float closeEnough = 0.5f;

    public float maxSpeed = 10f;
    public float accelRate = 0.1f;
    public float turningRate = 0.1f;

    private Target _target;
    private Vector3 _destination = Vector3.zero;
    private float _retargetTime;
    private float _desiredSpeed;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Target t = targetingSystem.GetTarget(transform);
            if (t == null)
            {
                return;
            }
            _target = t;
        }
        Transform target = _target.transform;

        if (Time.time > _retargetTime)
        {
            _retargetTime = float.MaxValue;

            Vector3 offset = GetNewDestinationOffset();
            _destination = target.position + offset;
            _desiredSpeed = maxSpeed;
        }

        Vector3 directionToTarget;
        if (_destination != Vector3.zero)
        {
            directionToTarget = _destination - transform.position;
            float distance = Vector3.Distance(_destination, transform.position);
            if (distance < closeEnough)
            {
                _retargetTime = Time.time + idleTime;
                _desiredSpeed = 0f;
                _destination = Vector3.zero;
            }
        }
        else
        {
            directionToTarget = target.transform.position - transform.position;
        }

        Quaternion q = Quaternion.LookRotation(transform.forward, directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, turningRate);

        float currentSpeed = _rb.velocity.magnitude;
        float speed = Mathf.Lerp(currentSpeed, _desiredSpeed, accelRate);
        _rb.velocity = transform.up * speed;

    }

    private Vector3 GetNewDestinationOffset()
    {
        Vector3 random = Random.insideUnitCircle.normalized;
        random *= Random.Range(minTargetRange, maxTargetRange);
        return random;
    }

    private void OnDrawGizmos()
    {
        if (_destination != Vector3.zero)
        {
            Gizmos.DrawWireSphere(_destination, 1f);
        }
    }
}
