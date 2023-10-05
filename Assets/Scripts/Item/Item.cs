using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    [SerializeField] int score = 1;
    protected internal int Score
    {
        get => score;
    }

    [SerializeField]
    GameObject _effectPrefab;
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        ItemManager.Instance.Collect(this);
    }
}
