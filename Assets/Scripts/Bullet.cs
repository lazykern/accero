using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
