using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    Center _center;

    public Rigidbody Rigidbody { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _center = Center.Instance;
        Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var direction = _center.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        Rigidbody.AddForce(direction * _center.pullForce);
    }

    void Update()
    {
    }
}
