using System;
using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class PlayerController : MonoBehaviour
{
    
    public static PlayerController Instance { get; private set; }
    
    bool _dragging;
    Vector3 _dragStartPosition;

    [SerializeField] Player _player;
    
    public Player Player
    {
        get => _player;
    }
    
    void Awake()
    {
        Instance = this;
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
    }

    void Update()
    {
        UpdatePlayerGun();
    }

    void UpdatePlayerGun()
    {
        if (MainManager.Instance.PlayerJoystick.Direction.magnitude > 0.1 && !_dragging)
        {
            _dragging = true;
        }

        if (MainManager.Instance.PlayerJoystick.Direction.magnitude > 0.1 && _dragging)
        {

            _player.DisplayLine();
            _player.transform.rotation = Quaternion.LookRotation(MainManager.Instance.PlayerJoystick.Direction.normalized);
        }

        if (MainManager.Instance.PlayerJoystick.Direction.magnitude != 0 || !_dragging)
            return;

        _player.TryShoot();
        _player.DisableLine();
        _dragging = false;
    }
}
