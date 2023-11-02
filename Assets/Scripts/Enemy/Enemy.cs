using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float _maxHealth = 5;

    [SerializeField]
    protected float _knockbackFromBullet = 20f;

    [SerializeField]
    protected float _knockbackFromPlayer = 20f;

    Color _color;

    float _health;

    Renderer _renderer;
    
    Collider _collider;

    internal bool _isAlive = true;

    protected Rigidbody rb { get; private set; }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    protected void Start()
    {
        _health = _maxHealth;
        _color = _renderer.material.color;
    }

    protected void Update()
    {
        
        if (_health <= 0)
        {
            return;
        }

        if (transform.localPosition.z != 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }

        _renderer.material.color = Color.Lerp(Color.black, _color, _health / _maxHealth);
    }
    
    protected internal void SetInitializedHealth(float health)
    {
        _maxHealth = health;
        _health = health;
    }

    internal void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Bullet":
                Hit(other.GetComponent<Bullet>().power);
                rb.velocity = Vector3.zero;
                rb.AddForce((other.transform.position - transform.position).normalized * _knockbackFromBullet * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
                break;
            case "Player":
                rb.AddForce((transform.position - other.transform.position).normalized * _knockbackFromPlayer * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
                break;
        }
    }

    void Hit(float damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
            _collider.enabled = false;
            _isAlive = false;
            EnemyManager.Instance.Kill(this);
    }
}

public enum EnemyType
{
    Acceleration,
    Velocity,
}