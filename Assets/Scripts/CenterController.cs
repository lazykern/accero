using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{
    [SerializeField] Joystick _joystick;
    [SerializeField] float baseVelocity = 0.2f;
    [SerializeField] float maxVelocity = 2f;
    
    Vector3 _velocity = Vector3.zero;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Center.Instance.rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (_joystick.IsOnDrag)
        {
            _velocity = _joystick.Direction.normalized * Mathf.Lerp(baseVelocity, maxVelocity, _joystick.Direction.magnitude);
        }
        else
        {
            _velocity = Vector3.zero;
        }
    }

    void Update()
    {
        Center.Instance.rb.velocity = _velocity;
    }
}
