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

    [SerializeField] float _gunKnockbackForce = 15f;
    [SerializeField] float _gunForce = 100f;
    [SerializeField] float _gunCooldown = 1f;

    float _lastShootTime;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] float _bulletScale = 0.5f;
    
    [SerializeField] float _knockbackFromEnemy = 10f;
    
    public float gunCooldown
    {
        get => _gunCooldown;
    }


    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _center = Center.Instance;
        rb = GetComponent<Rigidbody>();
        _lastShootTime = -_gunCooldown;
    }

    void Update()
    {
        if (transform.localPosition.z != 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Knock player back from enemy
            var direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction * _knockbackFromEnemy * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
        }
    }


    public void TryShoot()
    {
        if (IsInCooldown())
        {
            return;
        }
        Shoot();
    }

    void Shoot()
    {
        _lastShootTime = Time.time;
        
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.localScale = transform.lossyScale * _bulletScale;
        bullet.transform.forward = transform.forward;
        bullet.Rigidbody.AddForce(transform.forward * (_gunForce * GameManager.Instance.ScaleFactor()), ForceMode.Impulse);
        
        rb.AddForce(-transform.forward * (_gunKnockbackForce * GameManager.Instance.ScaleFactor()), ForceMode.Impulse);
    }
    public bool IsInCooldown()
    {
        return Time.time - _lastShootTime < _gunCooldown;
    }
    
    public bool CanShoot()
    {
        return !IsInCooldown();
    }
}
