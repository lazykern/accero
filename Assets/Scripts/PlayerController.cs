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
        if (_joystick.Direction.magnitude > 0.1 && !_dragging)
        {
            _dragging = true;
        }

        if (_joystick.Direction.magnitude > 0.1 && _dragging)
        {

            Player.Instance.DisplayLine();
            Player.Instance.transform.rotation = Quaternion.LookRotation(_joystick.Direction);
        }

        if (_joystick.Direction.magnitude != 0 || !_dragging)
            return;

        Player.Instance.TryShoot();
        Player.Instance.DisableLine();
        _dragging = false;
    }
}
