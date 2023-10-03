using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _ground;
    [SerializeField] LineRenderer _centerLine;
    Camera _camera;

    bool _dragging;
    Vector3 _dragStartPosition;
    
    
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        UpdateCenter();
        UpdatePlayerGun();
        
        Center.Instance.centripetalAcceleration += Input.mouseScrollDelta.y;
        
        UpdateCenterLine();
    }
    
    void FixedUpdate()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }
        var delta = Center.Instance.transform.position - Player.Instance.transform.position;
        var direction = delta.normalized;
        var force = direction * (Center.Instance.centripetalAcceleration * GameManager.Instance.ScaleFactor());

        Player.Instance.rb.AddForce(force, ForceMode.Acceleration);
    }

    void UpdateCenter()
    {
        
        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (!Physics.Raycast(ray, out var hit) || hit.collider.gameObject != _ground)
            return;

        Center.Instance.transform.position = Vector3.Lerp(Center.Instance.transform.position, hit.point, 0.1f);
        
        var direction = hit.point - Center.Instance.transform.position;
            
        if (direction.magnitude != 0 && direction != Vector3.zero)
        {
            Center.Instance.transform.rotation = Quaternion.Lerp(Center.Instance.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }

    void UpdateCenterLine()
    {
        _centerLine.SetPosition(0, Center.Instance.transform.position);
        _centerLine.SetPosition(1, Player.Instance.transform.position);
        
        
        
        if (Input.GetMouseButtonDown(0))
        {
            _centerLine.enabled = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _centerLine.enabled = false;
        }
    }

    void UpdatePlayerGun()
    {
        
        if (Input.GetMouseButtonDown(2))
        {
            _dragging = true;
            _dragStartPosition = Input.mousePosition;
            
            Player.Instance.DisableLine();
        }
        
        if (_dragging)
        {
            var dragDelta = Input.mousePosition - _dragStartPosition;
            var dragDirection = dragDelta.normalized;
            
            Player.Instance.transform.rotation = Quaternion.LookRotation(dragDirection);
            Player.Instance.DisplayLine();
        }

        if (Input.GetMouseButtonUp(2))
        {
            _dragging = false;
            Player.Instance.DisableLine();
            
            Player.Instance.TryShoot();
        }
    }
}


