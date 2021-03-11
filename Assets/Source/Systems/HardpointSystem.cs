using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardpointSystem : MonoBehaviour
{
    public RuntimeTargetSet targetingList;

    private List<Hardpoint> _hardpoints;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _hardpoints = new List<Hardpoint>(GetComponentsInChildren<Hardpoint>());
    }

    // Update is called once per frame
    void Update()
    {
        if (_hardpoints == null)
        {
            return;
        }
        foreach (Hardpoint hardpoint in _hardpoints)
        {
            hardpoint.Target = null;
            foreach (Target target in targetingList.Items)
            {
                // Until we handle removed targets
                if (target == null)
                {
                    continue;
                }

                if (hardpoint.CanSeePoint(target.transform.position))
                {
                    if (hardpoint.Target == null)
                    {
                        hardpoint.Target = target.gameObject;
                    }
                    else
                    {
                        Transform currentTarget = hardpoint.Target.transform;
                        float currentDistance = Vector3.Distance(currentTarget.position, _transform.position);
                        float newDistance = Vector3.Distance(target.transform.position, _transform.position);
                        if (currentDistance > newDistance)
                        {
                            hardpoint.Target = target.gameObject;
                        }
                    }
                }
            }
            hardpoint.AbleToFire = hardpoint.Target != null;
        }
    }
}
