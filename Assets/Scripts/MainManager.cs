using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Singleton]
public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    [SerializeField] Joystick acceleratorJoystick;
    internal Joystick AcceleratorJoystick
    {
        get => acceleratorJoystick;
    }

    [SerializeField] Joystick playerJoystick;

    [SerializeField] GameObject game;
    [SerializeField] GameObject gamePrefab;

    internal Joystick PlayerJoystick
    {
        get => playerJoystick;
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) Destroy(gameObject);
    }
    
    public void Restart()
    {
        var position = game.transform.position;
        var rotation = game.transform.rotation;
        var scale = game.transform.localScale;
        Destroy(game.gameObject);
        
        game = Instantiate(gamePrefab, position, rotation);
        game.transform.localScale = scale;
    }
    
    public bool IsGameRunning()
    {
        return game != null;
    }
}
