using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Singleton]
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] int maxEnemies = 5;
    [SerializeField] public float SpawnInterval = 10f;
    [SerializeField] float baseHealth = 2f;
    [SerializeField] float healthMultiplier = 1.25f;
    [SerializeField] float healthIncreaseInterval = 20f;
    
    [SerializeField] AccelerationEnemy accelerationEnemyPrefab;
    [SerializeField] VelocityEnemy velocityEnemyPrefab;
    
    
    [SerializeField] Transform accelerationEnemyDestroyLocation;
    [SerializeField] Transform velocityEnemyDestroyLocation;


    public float SpawnTimer { get; private set; }

    public float HealthIncreaseTimer { get; private set; }

    public EnemyType NextEnemyType { get; private set; }

    protected internal int AccelerationEnemyKillCount;
    protected internal int VelocityEnemyKillCount;
    

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        SpawnTimer = SpawnInterval;
        HealthIncreaseTimer = healthIncreaseInterval;
        NextEnemyType = RandomEnemyType();
    }
    
    void FixedUpdate()
    {
        SpawnTimer -= Time.fixedDeltaTime;
        HealthIncreaseTimer -= Time.fixedDeltaTime;
        
        if (SpawnTimer <= 0 && transform.childCount < maxEnemies)
        {
            SpawnTimer = SpawnInterval;
            SpawnEnemy();
        }

        if (!(HealthIncreaseTimer <= 0))
            return;

        HealthIncreaseTimer = healthIncreaseInterval;
        baseHealth *= healthMultiplier;
    }

    static EnemyType RandomEnemyType()
    {
        return Random.Range(0f,1f) < 0.5f ? EnemyType.Acceleration : EnemyType.Velocity;
    }

    void SpawnEnemy()
    {
        Enemy enemy = NextEnemyType == EnemyType.Acceleration ? Instantiate(accelerationEnemyPrefab, transform) : Instantiate(velocityEnemyPrefab, transform);
        
        enemy.SetInitializedHealth(baseHealth);
        enemy.transform.localPosition = Vector3.zero;
        
        NextEnemyType = RandomEnemyType();
    }
    
    protected internal void Kill(Enemy enemy)
    {
        ScoreManager.Instance.AddScore(1);

        var destroyLocation = (enemy is AccelerationEnemy ? accelerationEnemyDestroyLocation?.position : velocityEnemyDestroyLocation?.position) ?? (Vector3)enemy.transform.position;
        switch (enemy)
        {
            case AccelerationEnemy _:
                AccelerationEnemyKillCount++;
                break;
            case VelocityEnemy _:
                VelocityEnemyKillCount++;
                break;
        }
        
        StartCoroutine(Utils.DestroyItem(enemy, destroyLocation));
    }
}
