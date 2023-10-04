using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    Joystick _joystick;

    bool _dragging;

    Vector3 _dragStartPosition;

    void Update()
    {
        UpdatePlayerGun();
    }

    void UpdatePlayerGun()
    {
        if (_joystick.Direction.magnitude > 0 && !_dragging)
        {
            _dragging = true;
        }

        if (_joystick.Direction.magnitude > 0 && _dragging)
        {
            var dragDirection = _joystick.Direction.magnitude == 0
                ? new Vector2(transform.right.x, transform.right.y)
                : _joystick.Direction;
            Player.Instance.transform.rotation = Quaternion.LookRotation(dragDirection);
            Player.Instance.DisplayLine();
        }
        
        if (_joystick.Direction.magnitude != 0 || !_dragging)
            return;

        Player.Instance.TryShoot();
        Player.Instance.DisableLine();
        _dragging = false;
    }
}
