using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pointer : MonoBehaviour
{
    Camera _camera;
    [SerializeField] float _fadeSpeed = 1f;
    [SerializeField] float _fadeAt = 5f;
    [SerializeField] float _minimumAlpha = 0.1f;
    [SerializeField] MeshRenderer _meshRenderer;
    
    float _initializedTime;
    
    void Start()
    {
        _camera = Camera.main;
        _initializedTime = Time.time;
    }

    void Update()
    {
        // Look at the camera but keep the y rotation fixed
        transform.LookAt(_camera.transform);
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
    }

    void FixedUpdate()
    {
        if (!(Time.time - _initializedTime > _fadeAt))
            return;
        
        if (_meshRenderer.material.color.a <= _minimumAlpha)
            return;
        
        float alpha = Mathf.Lerp(_meshRenderer.material.color.a, _minimumAlpha, _fadeSpeed * Time.deltaTime);
        _meshRenderer.material.color = new Color(_meshRenderer.material.color.r, _meshRenderer.material.color.g, _meshRenderer.material.color.b, alpha);
    }
}
