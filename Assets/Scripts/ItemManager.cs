using System.Collections;
using JetBrains.Annotations;
using TMPro;
using Unity.Collections;
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

    [SerializeField]
    [CanBeNull]
    Transform pointItemDestroyLocation;
    
    [SerializeField]
    [CanBeNull]
    Transform powerItemDestroyLocation;
    
    float _spawnTimer;
    float _powerItemTimer;

    protected internal int CollectedPointItems;
    protected internal int CollectedPowerItems;

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
    
    protected internal void Collect(Item item)
    {
        ScoreManager.Instance.AddScore(item.Score);
        
        var destroyLocation = (item is PowerItem ? powerItemDestroyLocation?.position : pointItemDestroyLocation?.position) ?? (Vector3)item.transform.position;
        switch (item)
        {
            case PointItem:
                CollectedPointItems++;
                break;
            case PowerItem:
                CollectedPowerItems++;
                break;
            default:
                destroyLocation = item.transform.position;
                break;
        }

        StartCoroutine(Utils.DestroyItem(item, destroyLocation));
    }

}
