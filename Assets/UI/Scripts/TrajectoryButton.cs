using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrajectoryButton : MonoBehaviour
{
    Button _button;
    Image _image;
    
    [SerializeField]
    Color _enabledColor = Color.green;
    
    void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }
    
    void Update()
    {
        _image.color = MainManager.trajectoryEnabled ? Color.green : Color.white;
    }
}
