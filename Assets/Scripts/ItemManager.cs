using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField]
    GameObject pointItemPrefab;

    [SerializeField]
    GameObject powerItemPrefab;
    
    [SerializeField]
    Collider2D spawnArea;

    [SerializeField]
    Collider2D excludeArea;

    [SerializeField]
    int maxItems = 5;

    [SerializeField]
    float spawnInterval = 10f;
    
    [SerializeField]
    float powerItemInterval = 25f;
    
    float _spawnTimer;
    float _powerItemTimer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _spawnTimer = spawnInterval;
    }

    void FixedUpdate()
    {
        _spawnTimer -= Time.fixedDeltaTime;
        _powerItemTimer -= Time.fixedDeltaTime;

        if (!(_spawnTimer <= 0) || transform.childCount >= maxItems)
            return;
        
        _spawnTimer = spawnInterval;
        
        if (_powerItemTimer <= 0)
        {
            _powerItemTimer = powerItemInterval;
            SpawnPowerItem();
        }
        else
        {
            SpawnPointItem();
        }
    }

    Vector3 GetSpawnPosition()
    {
        var spawnPosition = new Vector2(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));

        if (!excludeArea.OverlapPoint(spawnPosition))
            return new Vector3(spawnPosition.x, spawnPosition.y, transform.position.z);

        if (Random.value > 0.5f)
        {
            spawnPosition.x = Random.value > 0.5f ? spawnArea.bounds.min.x : spawnArea.bounds.max.x;
        }
        else
        {
            spawnPosition.y = Random.value > 0.5f ? spawnArea.bounds.min.y : spawnArea.bounds.max.y;
        }

        return new Vector3(spawnPosition.x, spawnPosition.y, transform.position.z);
    }

    void SpawnPointItem()
    {
        var pointItem = Instantiate(pointItemPrefab, transform);
        pointItem.transform.position = GetSpawnPosition();
    }
    
    void SpawnPowerItem()
    {
        var powerItem = Instantiate(powerItemPrefab, transform);
        powerItem.transform.position = GetSpawnPosition();
    }
    
    protected internal static void Collect(Item item)
    {
        Destroy(item.gameObject);
    }
}
