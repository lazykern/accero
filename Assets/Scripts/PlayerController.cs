using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _ground;
    Camera _camera;

    Center _center;
    
    void Start()
    {
        _camera = Camera.main;
        _center = Center.Instance;
    }

    void Update()
    {
        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        if (Physics.Raycast(ray, out var hit) && hit.collider.gameObject == _ground)
        {
            if (Input.GetMouseButton(0))
            {
                _center.transform.position = Vector3.Lerp(_center.transform.position, hit.point, 0.1f);
            }
        }
        
        float pullForce = _center.pullForce + Input.mouseScrollDelta.y;
        _center.pullForce = Math.Clamp(pullForce, 0, 10);
    }
}
