using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Singleton]
public class Player : MonoBehaviour
{
    [SerializeField]
    int power = 1;
    
    [SerializeField]
    float gunPower = 1f;
    
    [SerializeField]
    float gunPowerIncreaseFactor = 1.25f;
    
    [SerializeField]
    int _bulletCount = 1;
    
    [SerializeField]
    int _maxBulletCount = 5;
    
    [SerializeField]
    float _gunKnockbackForce = 15f;

    [SerializeField]
    float _gunForce = 100f;

    [SerializeField]
    float _gunCooldown = 1f;

    [SerializeField]
    Bullet _bulletPrefab;

    [SerializeField]
    float _knockbackFromEnemy = 10f;


    float _lastShootTime;

    LineRenderer _lineRenderer;

    public static Player Instance { get; private set; }

    public Rigidbody rb { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
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
        switch (other.gameObject.tag)
        {
        case "Enemy":
            var direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction * _knockbackFromEnemy * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
            break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
         case "PowerItem": 
             PowerUp();
             break;
         case "PointItem":
             break;
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

    public void DisplayLine()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, Instance.transform.position);
        _lineRenderer.SetPosition(1, Instance.transform.position + Instance.transform.forward * (10 * GameManager.Instance.ScaleFactor()));

        _lineRenderer.startColor = CanShoot() ? Color.green : Color.red;
    }

    public void DisableLine()
    {
        _lineRenderer.enabled = false;
    }

    void Shoot()
    {
        _lastShootTime = Time.time;

        float degree = 0;
        for (int i = 0; i < _bulletCount; i++)
        {
            var bullet = Instantiate(_bulletPrefab, transform.position + transform.forward * (0.5f * GameManager.Instance.ScaleFactor()), transform.rotation);
            bullet.power = gunPower;
            bullet.transform.localScale = Vector3.one * (GameManager.Instance.ScaleFactor());
            
            bullet.transform.forward = transform.forward;

            if (i % 2 != 0)
            {
                degree += 2.5f;
            }
            
            if (i > 0)
            {
                bullet.transform.forward = Quaternion.AngleAxis((float)(degree * Math.Pow(-1, i)), bullet.transform.right) * bullet.transform.forward;
            }
            bullet.Rigidbody.AddForce(bullet.transform.forward * (_gunForce * GameManager.Instance.ScaleFactor()), ForceMode.Impulse);
        }
        
        rb.AddForce(-transform.forward * (_gunKnockbackForce * GameManager.Instance.ScaleFactor()), ForceMode.Impulse);
    }

    void PowerUp()
    {
        power += 1;
        gunPower *= gunPowerIncreaseFactor;
        
        if (power % 5 == 0 && _bulletCount < _maxBulletCount)
        {
            _bulletCount += 1;
        }
    }

    bool IsInCooldown()
    {
        return Time.time - _lastShootTime < _gunCooldown;
    }

    public bool CanShoot()
    {
        return !IsInCooldown();
    }
}
