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


    public new Rigidbody rb { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
