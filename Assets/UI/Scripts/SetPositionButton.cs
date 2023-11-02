using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetPositionButton : MonoBehaviour
{
    Button _button;
    Image _image;
    
    [SerializeField] Color _toggleColor = Color.green;
    
    void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }
    
    void Update()
    {
        _button.interactable = MainManager.Instance.IsGameRunning() && GameManager.Instance.State != GameState.Playing;
        _image.color = GameManager.Instance.State == GameState.SetPosition ? _toggleColor : Color.white;
    }
}
