using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float _maxHealth = 5;

    [SerializeField]
    protected int _knockbackFromBullet = 20;

    [SerializeField]
    protected int _knockbackFromPlayer = 10;

    Color _color;

    float _health;

    Renderer _renderer;

    protected Rigidbody rb { get; private set; }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        _health = _maxHealth;
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
    }

    protected void Update()
    {

        if (transform.localPosition.z != 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
    }

    internal void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Bullet":
                Hit(other.GetComponent<Bullet>().power);
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
        UpdateFromHealth();
    }

    void UpdateFromHealth()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        var material = _renderer.material;
        material.color = Color.Lerp(_color, Color.black, 1 - _health / _maxHealth);
    }
}
