using UnityEngine;

public class PointItem : MonoBehaviour
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

    public void OnPickup()
    {
        throw new System.NotImplementedException();
    }
}
