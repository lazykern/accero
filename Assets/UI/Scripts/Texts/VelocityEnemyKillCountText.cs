using TMPro;
using UnityEngine;

public class VelocityEnemyKillCountText : MonoBehaviour
{
    TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = EnemyManager.Instance.VelocityEnemyKillCount.ToString();
    }
}
