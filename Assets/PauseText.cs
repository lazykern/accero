using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseText : MonoBehaviour
{
    TextMeshProUGUI _text;
    
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = GameManager.IsPaused() ? "Resume" : "Pause";
    }
}
