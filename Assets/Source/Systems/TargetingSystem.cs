using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : ScriptableObject
{
    public virtual Target GetTarget(Transform origin)
    {
        return null;
    }
}
