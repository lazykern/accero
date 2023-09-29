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

    Rigidbody _rigidbody;
    
    [SerializeField] GameObject _gun;
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
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var direction = _center.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        _rigidbody.AddForce(direction * _center.pullForce);
    }

    public void SetCannonDirection(Vector3 direction)
    {
        transform.forward = direction;
    }
    
    public void ShootCannon()
    {
        var bullet = Instantiate(_bulletPrefab, _gun.transform.position, Quaternion.identity);
        bullet.transform.localScale = transform.lossyScale * _bulletScale;
        bullet.transform.forward = transform.forward;
        bullet.Rigidbody.AddForce(transform.forward * _gunForce, ForceMode.Impulse);
        
        _rigidbody.AddForce(-transform.forward * _gunRecoil, ForceMode.Impulse);
    }
}
