using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NearbyTargetingSystem : TargetingSystem
{
    public float maxRange;
    public RuntimeTargetSet targetSet;

    public override Target GetTarget(Transform origin)
    {
        Target selectedTarget = null;
        float currentDistance = float.MaxValue;
        foreach (Target target in targetSet.Items)
        {
            // Until we handle removed targets
            if (target == null)
            {
                continue;
            }

            float newDistance = Vector3.Distance(target.transform.position, origin.position);
            if (currentDistance > newDistance)
            {
                selectedTarget = target;
                currentDistance = newDistance;
            }
        }

        return selectedTarget;
    }
}
