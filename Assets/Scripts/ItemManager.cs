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
    
    float _spawnTimer;

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

        if (!(_spawnTimer <= 0) || transform.childCount >= maxItems)
            return;

        _spawnTimer = spawnInterval;
        SpawnItem();
    }

    void SpawnItem()
    {
        var spawnPosition = new Vector2(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
        
        if (excludeArea.OverlapPoint(spawnPosition))
        {
            if (Random.value > 0.5f)
            {
                spawnPosition.x = Random.value > 0.5f ? spawnArea.bounds.min.x : spawnArea.bounds.max.x;
            }
            else
            {
                spawnPosition.y = Random.value > 0.5f ? spawnArea.bounds.min.y : spawnArea.bounds.max.y;
            }
        }
        
        var item = Random.value > 0.2f ? Instantiate(pointItemPrefab, transform) : Instantiate(powerItemPrefab, transform);
        item.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, transform.position.z);
    }
}
