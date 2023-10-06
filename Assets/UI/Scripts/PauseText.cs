using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseText : MonoBehaviour
{
    TextMeshProUGUI _text;
    
    [SerializeField] string textOnPause = "Resume";
    [SerializeField] string textOnPlay = "Pause";
    
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = GameManager.Instance.State == GameState.Paused ? textOnPause : textOnPlay;
    }
}
