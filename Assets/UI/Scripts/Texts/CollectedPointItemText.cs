using TMPro;
using UnityEngine;

public class CollectedPointItemText : MonoBehaviour
{
    TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = ItemManager.Instance.CollectedPointItems.ToString();
    }
}
