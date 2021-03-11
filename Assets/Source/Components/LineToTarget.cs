using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToTarget : MonoBehaviour, IHoldsTarget
{
    public float scaleMultiplier = 1f;
    private Transform _target;

    public void SetTarget(GameObject target)
    {
        _target = target.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector3 parentPosition = transform.parent.position;
            Vector3 targetPosition = _target.position;
            Target target = _target.GetComponent<Target>();
            if (target != null)
            {
                targetPosition = target.GetTargetPoint(transform.parent.gameObject.GetInstanceID());
            }
            Vector3 halfway = (parentPosition + targetPosition) / 2;
            float distance = Vector3.Distance(parentPosition, targetPosition);

            transform.position = halfway;
            Vector3 scale = transform.localScale;
            scale.y = distance * scaleMultiplier;
            transform.localScale = scale;

            Vector3 directionToTarget = targetPosition - parentPosition;
            Quaternion rotation = Quaternion.LookRotation(transform.forward, directionToTarget);
            transform.rotation = rotation;
        }
    }
}
