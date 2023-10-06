using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.timeScale == 0)
        {
            GameManager.Resume();
        }
        else
        {
            GameManager.Pause();
        }
    }
}
