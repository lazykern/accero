using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    internal int _maxHealth = 5;

    [SerializeField]
    internal int _knockbackFromBullet = 20;

    [SerializeField]
    internal int _knockbackFromPlayer = 10;

    Color _color;

    internal int _health;

    Renderer _renderer;

    public Rigidbody rb { get; private set; }

    internal void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    internal void Start()
    {
        _health = _maxHealth;
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
    }

    internal void Update()
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
                _health--;
                UpdateFromHealth();
                rb.AddForce((other.transform.position - transform.position).normalized * _knockbackFromBullet * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
                break;
            case "Player":
                rb.AddForce((transform.position - other.transform.position).normalized * _knockbackFromPlayer * GameManager.Instance.ScaleFactor(), ForceMode.Impulse);
                break;
        }
    }

    internal void UpdateFromHealth()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        var material = _renderer.material;
        material.color = Color.Lerp(_color, Color.black, 1 - (float)_health / _maxHealth);
    }
}
