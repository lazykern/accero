using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Singleton]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    Center _center;

    public Rigidbody rb { get; private set; }

    [SerializeField] float _gunRecoil = 1f;
    [SerializeField] float _gunForce = 2f;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] float _bulletScale = 0.5f;


    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _center = Center.Instance;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.localPosition.z != 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
    }

    void FixedUpdate()
    {
        var direction = _center.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        rb.AddForce(direction * _center.pullForce);
    }

    public void ShootCannon()
    {
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.localScale = transform.lossyScale * _bulletScale;
        bullet.transform.forward = transform.forward;
        bullet.Rigidbody.AddForce(transform.forward * _gunForce, ForceMode.Impulse);
        
        rb.AddForce(-transform.forward * _gunRecoil, ForceMode.Impulse);
    }
}
