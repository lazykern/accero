using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnIndicator : MonoBehaviour
{
    [SerializeField] Sprite accelerationEnemySprite;
    [SerializeField] Sprite velocityEnemySprite;
    
    Image _image;
    
    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void SetSprite(EnemyType enemyType)
    {
        _image.sprite = enemyType switch
        {
            EnemyType.Acceleration => accelerationEnemySprite,
            EnemyType.Velocity => velocityEnemySprite,
            _ => accelerationEnemySprite
        };
    }

    void SetFill(float fill)
    {
        _image.fillAmount = fill;
    }

    void Update()
    {
        SetFill(EnemyManager.Instance.SpawnTimer / EnemyManager.Instance.SpawnInterval);
        SetSprite(EnemyManager.Instance.NextEnemyType);
    }
}
