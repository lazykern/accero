using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    RectTransform _rectTransform;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] float horizontalSpacing = 4f;
    [SerializeField] Player player;

    readonly List<Heart> _hearts = new List<Heart>();
    
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        var center = _rectTransform.rect.center;
        var heartSize = heartPrefab.GetComponent<RectTransform>().rect.size;
        float heartWidth = heartSize.x;
        float startX = center.x - (heartWidth * player.maxHealth / 2f) - (horizontalSpacing * (player.maxHealth - 1) / 2f);
        
        for (int i = 0; i < player.maxHealth; i++)
        {
            var heart = Instantiate(heartPrefab, transform).GetComponent<Heart>();
            heart.transform.localPosition = new Vector3(startX + (i * (heartWidth + horizontalSpacing) + heartWidth / 2f), center.y, 0);
            _hearts.Add(heart);
        }
        
    }
    
    void Update()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[_hearts.Count - 1 - i].Image.color = i < player.Health ? Color.white : new Color(1, 1, 1, 0.1f);
        }
    }
}
