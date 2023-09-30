using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    internal int _hp = 1;
    
    Rigidbody _rigidbody;
    public Rigidbody rb
    {
        get => _rigidbody;
    }

    internal void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    internal void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _hp--;
            Debug.Log($"HP: {_hp}");
        }
    }

    internal void Update()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
        
        if (transform.localPosition.y != 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        }
    }
}
