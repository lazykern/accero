using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Center : MonoBehaviour
{
    public static Center Instance { get; private set; }
    
    [Range(0, 10)]
    public float pullForce = 5f;

    Rigidbody _rigidbody;
    public Rigidbody rigidbody
    {
        get => _rigidbody;
    }
    
    Transform _transform;
    public Transform transform
    {
        get => _transform;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }
}
