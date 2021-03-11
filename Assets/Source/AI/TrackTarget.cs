using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackTarget : MonoBehaviour, IHoldsTarget
{
    public float maxSpeed = 10f;
    public float maxTurn = 180f;
    public float accelRate = 0.1f;
    public float turningRate = 0.1f;
    public float proximity = 4f;
    public bool slowdownToTurn = true;
    public TargetingSystem targetingSystem;

    public UnityEvent lostTarget;

    private Transform _target;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector3 directionToTarget = _target.position - transform.position;
            Quaternion q = Quaternion.LookRotation(transform.forward, directionToTarget);
            Quaternion desired = Quaternion.Slerp(transform.rotation, q, turningRate);

            if (_rb != null)
            {
                float currentSpeed = _rb.velocity.magnitude;
                float speed = Mathf.Lerp(currentSpeed, maxSpeed, accelRate);
                float d = Vector3.Dot(transform.up, directionToTarget.normalized);
                if (slowdownToTurn && (d < 0.9f && d > 0.2f))
                {
                    speed *= 0.95f;
                }
                if (d < 0f)
                {
                    float squareDistance = directionToTarget.sqrMagnitude;
                    if (squareDistance < proximity * proximity)
                    {
                        desired = transform.rotation;
                    }
                }
                _rb.velocity = transform.up * speed;
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, maxTurn * Time.deltaTime);
        }
        else if (targetingSystem != null)
        {
            Target target = targetingSystem.GetTarget(transform);
            if (target != null)
            {
                _target = target.transform;
            }
        }
        else
        {
            lostTarget.Invoke();
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target.transform;
    }
}
