﻿using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    [SerializeField]
    float moveThreshold = 1;

    Vector2 _initialAnchoredPosition;

    public float MoveThreshold
    {
        get => moveThreshold;
        set => moveThreshold = Mathf.Abs(value);
    }

    protected override void Start()
    {
        base.Start();
        MoveThreshold = moveThreshold;
        _initialAnchoredPosition = background.anchoredPosition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        background.anchoredPosition = _initialAnchoredPosition;
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            var difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}
