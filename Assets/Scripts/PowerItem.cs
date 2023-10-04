using UnityEngine;

public class PowerItem : MonoBehaviour
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
        Destroy(gameObject);
    }
}
