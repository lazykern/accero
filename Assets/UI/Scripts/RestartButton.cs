using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    Button _button;
    
    void Start()
    {
        _button = GetComponent<Button>();
    }
    
    void Update()
    {
        _button.interactable = MainManager.Instance.IsGameRunning() && GameManager.Instance.State != GameState.Start &&
                               GameManager.Instance.State != GameState.SetPosition;
    }
}
