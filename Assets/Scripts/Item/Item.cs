using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    int _score = 1;
    
    [SerializeField]
    GameObject _effectPrefab;
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        ScoreManager.Instance.AddScore(_score);
    }
    
    protected virtual void OnPickup()
    {
        if (_effectPrefab != null)
            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
