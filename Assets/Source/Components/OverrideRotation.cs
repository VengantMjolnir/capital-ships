using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideRotation : MonoBehaviour
{
    public Vector3 rotation;

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion q = Quaternion.Euler(rotation);
        transform.rotation = q;
    }
}
