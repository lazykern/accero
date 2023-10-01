using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _ground;
    Camera _camera;
    LineRenderer _lineRenderer;

    bool _dragging;
    Vector3 _dragStartPosition;
    
    
    void Start()
    {
        _camera = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateCenter();
        UpdatePlayerGun();
        
        float centripetalAcceleration = Center.Instance.centripetalAcceleration + Input.mouseScrollDelta.y;
        Center.Instance.centripetalAcceleration = Math.Clamp(centripetalAcceleration, 0, 100);
    }
    
    void FixedUpdate()
    {
        var direction = Center.Instance.transform.position - Player.Instance.transform.position;
        Player.Instance.rb.AddForce((direction/GameManager.Instance.ScaleFactor()) * (Center.Instance.centripetalAcceleration * GameManager.Instance.ScaleFactor()) , ForceMode.Acceleration);
    }

    void UpdateCenter()
    {
        
        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (!Physics.Raycast(ray, out var hit) || hit.collider.gameObject != _ground)
            return;

        if (!Input.GetMouseButton(0))
            return;

        Center.Instance.transform.position = Vector3.Lerp(Center.Instance.transform.position, hit.point, 0.1f);

        var direction = hit.point - Center.Instance.transform.position;
            
        if (direction.magnitude != 0 && direction != Vector3.zero)
        {
            Center.Instance.transform.rotation = Quaternion.Lerp(Center.Instance.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }

    void UpdatePlayerGun()
    {
        
        if (Input.GetMouseButtonDown(2))
        {
            _dragging = true;
            _lineRenderer.enabled = true;
            
            _dragStartPosition = Input.mousePosition;
        }
        
        if (_dragging)
        {
            var dragDelta = Input.mousePosition - _dragStartPosition;
            var dragDirection = dragDelta.normalized;
            
            Player.Instance.transform.rotation = Quaternion.LookRotation(dragDirection);

            _lineRenderer.SetPosition(0, Player.Instance.transform.position);
            _lineRenderer.SetPosition(1, Player.Instance.transform.position + dragDirection * (10 * GameManager.Instance.ScaleFactor()));
            
            _lineRenderer.startColor = !Player.Instance.IsInCooldown() ? Color.green : Color.red;
        }

        if (Input.GetMouseButtonUp(2))
        {
            _dragging = false;
            _lineRenderer.enabled = false;
            
            Player.Instance.TryShoot();
        }
    }
}


