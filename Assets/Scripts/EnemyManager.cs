using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
    
    float _spawnTimer;
    float _healthIncreaseTimer;
    
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
        
        if (_healthIncreaseTimer <= 0)
        {
            _healthIncreaseTimer = healthIncreaseInterval;
            baseHealth *= healthMultiplier;
        }
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
    
    protected internal static void Kill(Enemy enemy)
    {
        ScoreManager.Instance.AddScore(1);
        Destroy(enemy.gameObject);
    }
}
