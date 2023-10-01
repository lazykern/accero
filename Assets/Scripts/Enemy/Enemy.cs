using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    internal int _hp = 1;

    [SerializeField]
    internal int _knockbackFromBullet = 20;
    
    [SerializeField]
    internal int _knockbackFromPlayer = 10;
    
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
        switch (other.gameObject.tag)
        {
            case "Bullet":
                _hp--;
                rb.AddForce((other.transform.position - transform.position).normalized * _knockbackFromBullet * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
                break;
            case "Player":
                rb.AddForce((transform.position - other.transform.position).normalized * _knockbackFromPlayer * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
                break;
        }
    }
    
    internal void Update()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
        
        if (transform.localPosition.z != 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
    }
}