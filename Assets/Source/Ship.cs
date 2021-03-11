using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    public SpriteRenderer shipSprite;
    public Sprite normal;
    public Sprite thrusting;

    [Header("Input")]
    public string thrust = "Thrust";
    public string reverse = "Reverse";
    public string rotate = "Horizontal";
    [Header("Movement Values")]
    public float acceleration = 10.0f;
    public float torque = 1f;

    private float _thrust = 0f;
    private float _rotate = 0f;
    private Rigidbody2D _rb;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(thrust))
        {
            shipSprite.sprite = thrusting;
            _thrust = 1f;
        }
        else
        {
            shipSprite.sprite = normal;
            _thrust = 0f;
            if (Input.GetButton(reverse))
            {
                _thrust = -0.5f;
            }
        }

        _rotate = -Input.GetAxis(rotate);
    }


    public void FixedUpdate()
    {
        if (System.Math.Abs(_thrust) > float.Epsilon)
        {
            _rb.AddForce(_transform.right * acceleration * _thrust);
        }

        if (System.Math.Abs(_rotate) > float.Epsilon)
        {
            _rb.AddTorque(_rotate * torque);
        }
    }
}
