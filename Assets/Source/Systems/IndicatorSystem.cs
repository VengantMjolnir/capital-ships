using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IndicatorSystem : MonoBehaviour
{
    public Vector2 bounds = Vector2.one;
    public Transform target;

    private Transform _transform;
    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Application.isPlaying)
        {
            if (target == null)
            {
                Destroy(gameObject);
            }
        }

        Camera cam = Camera.main;
        if (cam != null)
        {
            bounds.y = cam.orthographicSize;
            bounds.x = cam.orthographicSize * cam.aspect;
        }
        if (target != null && cam != null)
        {
            Vector3 camPosition = cam.transform.position;
            Vector3 position = target.position - camPosition;
            Vector3 angles = Vector3.zero;
            int h = 0;
            int v = 0;
            if (position.x > bounds.x)
            {
                position.x = bounds.x;
                angles.z = -90;
                h = 1;
            }
            if (position.x < -bounds.x)
            {
                position.x = -bounds.x;
                angles.z = 90;
                h = -1;
            }
            if (position.y > bounds.y)
            {
                position.y = bounds.y;
                angles.z = 0;
                v = 1;
            }
            if (position.y < -bounds.y)
            {
                position.y = -bounds.y;
                angles.z = 180;
                v = -1;
            }

            if (v != 0)
            {
                angles.z += -45 * h * v;
            }

            position.x += camPosition.x;
            position.y += camPosition.y;
            position.z = _transform.position.z;
            _transform.position = position;
            _transform.rotation = Quaternion.Euler(angles);

            if (_sprite != null)
            {
                _sprite.enabled = (h != 0 || v != 0);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 size = bounds * 2;
            Gizmos.DrawWireCube(cam.transform.position, size);
        }
    }
#endif
}
