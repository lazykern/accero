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
        if (GameManager.Instance.State != GameState.Playing)
            return;
        
        UpdatePlayerDirection();
    }

    void UpdatePlayerDirection()
    {
        if (MainManager.Instance.PlayerJoystick.Direction.magnitude > 0.1 && !_dragging)
        {
            _dragging = true;
        }

        if (MainManager.Instance.PlayerJoystick.Direction.magnitude > 0.1 && _dragging)
        {
            _player.DisplayLine();
            
            var joystickDirection = MainManager.Instance.PlayerJoystick.Direction.normalized;
            var gameRight = GameManager.Instance.transform.right;
            var gameUp = GameManager.Instance.transform.up;
            
            var direction = gameUp * joystickDirection.y + -gameRight * joystickDirection.x;
            _player.transform.forward = direction;
        }

        if (MainManager.Instance.PlayerJoystick.Direction.magnitude != 0 || !_dragging)
            return;

        _player.TryShoot();
        _player.DisableLine();
        _dragging = false;
    }
    
    internal static void Die()
    {
        GameManager.Instance.GameOver();
    }
}
