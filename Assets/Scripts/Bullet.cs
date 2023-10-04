using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float _destroyTime = 5f;
    
    [SerializeField] public float power = 1f;

    float _instantiateTime;

    public Rigidbody Rigidbody { get; private set; }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _instantiateTime = Time.time;
    }

    void Update()
    {
        if (Time.time - _instantiateTime > _destroyTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                Destroy(gameObject);
                break;
        }
    }
}
