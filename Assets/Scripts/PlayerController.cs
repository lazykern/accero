using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _ground;
    Camera _camera;
    LineRenderer _lineRenderer;

    bool cannonDragging;
    Vector3 cannonDraggingStartPosition;
    
    void Start()
    {
        _camera = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateCenterPosition();
        UpdateCannonTurning();
        
        float pullForce = Center.Instance.pullForce + Input.mouseScrollDelta.y;
        Center.Instance.pullForce = Math.Clamp(pullForce, 0, 10);
    }

    void UpdateCenterPosition()
    {
        
        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (!Physics.Raycast(ray, out var hit) || hit.collider.gameObject != _ground)
            return;

        if (Input.GetMouseButton(0))
        {
            Center.Instance.transform.position = Vector3.Lerp(Center.Instance.transform.position, hit.point, 0.1f);
        }
    }

    void UpdateCannonTurning()
    {
        if (Input.GetMouseButtonDown(2))
        {
            cannonDragging = true;
            _lineRenderer.enabled = true;
            
            cannonDraggingStartPosition = Input.mousePosition;
        }
        
        if (cannonDragging)
        {
            var dragDelta = Input.mousePosition - cannonDraggingStartPosition;
            var direction = dragDelta.magnitude > 0 ? dragDelta.normalized : transform.forward;
            Debug.DrawRay(Player.Instance.transform.position, new Vector3(direction.x, direction.y, 0) * 10, Color.red);
            Player.Instance.SetCannonDirection(new Vector3(direction.x, direction.y, 0));
            
            _lineRenderer.SetPosition(0, Player.Instance.transform.position);
            _lineRenderer.SetPosition(1, Player.Instance.transform.position + direction * 10);
        }

        if (Input.GetMouseButtonUp(2))
        {
            cannonDragging = false;
            _lineRenderer.enabled = false;
            
            Player.Instance.ShootCannon();
        }
    }
}


