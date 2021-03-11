using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowInBounds : MonoBehaviour
{
    public Vector2 bounds = Vector2.one;
    public Transform target;

    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 position = target.position;
            if (position.x > bounds.x)
            {
                position.x = bounds.x;
            }
            if (position.x < -bounds.x)
            {
                position.x = -bounds.x;
            }
            if (position.y > bounds.y)
            {
                position.y = bounds.y;
            }
            if (position.y < -bounds.y)
            {
                position.y = -bounds.y;
            }
            position.z = _transform.position.z;
            _transform.position = position;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 size = bounds * 2;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
#endif
}
