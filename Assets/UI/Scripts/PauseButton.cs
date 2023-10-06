using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IPointerUpHandler
{
    Button _button;
    
    void Start()
    {
        _button = GetComponent<Button>();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.State == GameState.Paused)
        {
            GameManager.Resume();
        }
        else
        {
            GameManager.Pause();
        }
    }
    
    void Update()
    {
        _button.interactable = MainManager.Instance.IsGameRunning();
    }
}
