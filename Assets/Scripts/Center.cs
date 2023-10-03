using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Singleton]
public class Center : MonoBehaviour
{
    public static Center Instance { get; private set; }
    
    [Range(0f, 100f)]
    public float centripetalAcceleration = 20f;

    Rigidbody _rigidbody;
    public new Rigidbody rigidbody
    {
        get => _rigidbody;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
