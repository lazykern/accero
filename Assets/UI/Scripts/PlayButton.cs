using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Button _button;
    
    void Start()
    {
        _button = GetComponent<Button>();
    }
    
    void Update()
    {
        _button.interactable = MainManager.Instance.IsGameRunning() && GameManager.Instance.State == GameState.Start;
    }
}
