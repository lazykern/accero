using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Singleton]
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    [SerializeField] float spawnInterval = 10f;
    
    [SerializeField] float baseHealth = 2f;
    [SerializeField] float healthMultiplier = 1.1f;
    [SerializeField] float healthIncreaseInterval = 20f;
    
    [SerializeField] AccelerationEnemy accelerationEnemyPrefab;
    [SerializeField] VelocityEnemy velocityEnemyPrefab;
    
    [SerializeField] int maxEnemies = 5;

    [SerializeField]
    Image enemySpawnBar;
    
    [SerializeField] Transform accelerationEnemyDestroyLocation;
    [SerializeField] Transform velocityEnemyDestroyLocation;
    
    
    float _spawnTimer;
    float _healthIncreaseTimer;

    protected internal int AccelerationEnemyKillCount;
    protected internal int VelocityEnemyKillCount;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _spawnTimer = spawnInterval;
        _healthIncreaseTimer = healthIncreaseInterval;
    }
    
    void FixedUpdate()
    {
        _spawnTimer -= Time.fixedDeltaTime;
        _healthIncreaseTimer -= Time.fixedDeltaTime;
        
        if (_spawnTimer <= 0 && transform.childCount < maxEnemies)
        {
            _spawnTimer = spawnInterval;
            SpawnEnemy();
        }

        if (!(_healthIncreaseTimer <= 0))
            return;

        _healthIncreaseTimer = healthIncreaseInterval;
        baseHealth *= healthMultiplier;
    }

    void Update()
    {
        enemySpawnBar.fillAmount = 1 - _spawnTimer / spawnInterval;
    }

    void SpawnEnemy()
    {
        Enemy enemy = Random.value > 0.5f ? Instantiate(accelerationEnemyPrefab, transform) : Instantiate(velocityEnemyPrefab, transform);
        enemy.SetInitializedHealth(baseHealth);
        enemy.transform.localPosition = Vector3.zero;
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
