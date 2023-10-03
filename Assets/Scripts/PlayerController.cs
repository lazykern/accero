using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LineRenderer _centerLine;

    [SerializeField] Joystick _joystick;

    bool _dragging;
    Vector3 _dragStartPosition;
    
    void Update()
    {
        UpdatePlayerGun();
        UpdateCenterLine();
    }
    
    void UpdatePlayerGun()
    {
        if (_joystick.Direction.magnitude > 0 && !_dragging)
        {
            _dragging = true;
        }
        
        switch (_joystick.Direction.magnitude)
        {
            case > 0 when _dragging:
            {
                var dragDirection = _joystick.Direction;
            
                Player.Instance.transform.rotation = Quaternion.LookRotation(dragDirection);
                Player.Instance.DisplayLine();
                break;
            }
            case 0 when _dragging:
                _dragging = false;
                Player.Instance.DisableLine();
                    
                Player.Instance.TryShoot();
                break;
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

}


