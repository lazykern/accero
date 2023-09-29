using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Center _center;
    Rigidbody _rigidbody;
    
    void Start()
    {
        _center = Center.Instance;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var direction = _center.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        _rigidbody.AddForce(direction * _center.pullForce);
    }

    void Update()
    {
    }
}
