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

    internal Camera Camera { get; private set; }
    
    internal static bool trajectoryEnabled = false;
    internal static int trajectoryLength = 50;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) Destroy(gameObject);
    }

    void Start()
    {
        if (game == null)
        {
            game = GameObject.FindWithTag("Game");
        }
        Camera = Camera.main;
        
        game.GetComponent<GameManager>().SetPosition();
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

    public static void Pause()
    {
        Time.timeScale = 0;
    }
    
    public static void Resume()
    {
        Time.timeScale = 1;
    }
    
    public bool IsGameRunning()
    {
        return game != null;
    }
    
    public static void ToggleTrajectory()
    {
        trajectoryEnabled = !trajectoryEnabled;
    }
    
    public static void IncreaseTrajectoryLength()
    {
        trajectoryLength += 10;
    }
    
    public static void DecreaseTrajectoryLength()
    {
        trajectoryLength -= Math.Max(trajectoryLength - 10, 10);
    }
}
