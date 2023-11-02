using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TrajectoryModButton : MonoBehaviour
{
    Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
    }

    void Update()
    {
        _button.interactable = MainManager.trajectoryEnabled;
    }
}
