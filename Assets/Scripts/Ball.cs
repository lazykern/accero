using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Center _center;
    Rigidbody _rigidbody;
    
    [SerializeField] bool _isDebug = false;
    
    
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
    
    void OnCollisionEnter(Collision other)
    {
        if (_isDebug)
        {
            Debug.Log($"Ball collided with {other.gameObject.name}");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        }
    }
}
