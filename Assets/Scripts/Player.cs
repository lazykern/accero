using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class Player : MonoBehaviour
{
    [SerializeField]
    float _gunKnockbackForce = 15f;

    [SerializeField]
    float _gunForce = 100f;

    [SerializeField]
    float _gunCooldown = 1f;

    [SerializeField]
    Bullet _bulletPrefab;

    [SerializeField]
    float _bulletScale = 0.5f;

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

        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.localScale = transform.lossyScale * _bulletScale;
        bullet.transform.forward = transform.forward;
        bullet.Rigidbody.AddForce(transform.forward * (_gunForce * GameManager.Instance.ScaleFactor()), ForceMode.Impulse);

        rb.AddForce(-transform.forward * (_gunKnockbackForce * GameManager.Instance.ScaleFactor()), ForceMode.Impulse);
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
