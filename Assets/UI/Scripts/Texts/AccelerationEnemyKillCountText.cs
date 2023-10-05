using TMPro;
using UnityEngine;

public class AccelerationEnemyKillCountText : MonoBehaviour
{
    TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = EnemyManager.Instance.AccelerationEnemyKillCount.ToString();
    }
}
