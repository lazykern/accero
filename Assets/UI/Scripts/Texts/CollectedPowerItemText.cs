using TMPro;
using UnityEngine;

public class CollectedPowerItemText : MonoBehaviour
{
    TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = ItemManager.Instance.CollectedPowerItems.ToString();
    }
}
