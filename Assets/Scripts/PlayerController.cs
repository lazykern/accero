using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _ground;
    [SerializeField] float _cannonRecoil = 10f;
    Camera _camera;

    bool cannonDragging;
    Vector3 cannonDraggingStartPosition;
    
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        UpdateCenterPosition();
        UpdateCannonAngle();
        
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

    void UpdateCannonAngle()
    {
        if (Input.GetMouseButtonDown(2))
        {
            cannonDraggingStartPosition = Input.mousePosition;
            cannonDragging = true;
        }
        
        if (cannonDragging)
        {
            var delta = Input.mousePosition - cannonDraggingStartPosition;
            var normalizedDelta = delta.normalized;
            Debug.DrawRay(Player.Instance.transform.position, new Vector3(normalizedDelta.x, normalizedDelta.y, 0) * 10, Color.red);
            Player.Instance.transform.forward = new Vector3(normalizedDelta.x, normalizedDelta.y, 0);
        }

        if (Input.GetMouseButtonUp(2))
        {
            cannonDragging = false;
            Player.Instance.Rigidbody.AddForce(-Player.Instance.transform.forward * _cannonRecoil, ForceMode.Impulse);
        }
    }
}


