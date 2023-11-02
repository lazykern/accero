using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Start;
    private GameState _previousState = GameState.Start;
    
    [SerializeField] List<GameObject> _activeOnStart;
    [SerializeField] List<GameObject> _deactiveOnStart;
    
    [SerializeField] List<GameObject> _activeOnPlaying;
    [SerializeField] List<GameObject> _deactiveOnPlaying;
    
    [SerializeField] List<GameObject> _activeOnPaused;
    [SerializeField] List<GameObject> _deactiveOnPaused;
    
    [SerializeField] List<GameObject> _activeOnGameOver;
    [SerializeField] List<GameObject> _deactiveOnGameOver;
    
    [SerializeField] BoxCollider _gameArea;

    public BoxCollider GameArea
    {
        get => _gameArea;
    }

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        UpdateGameObjects();
    }

    void FixedUpdate()
    {
        if (State == GameState.SetPosition)
        {
            DoSetPosition();
        }
    }

    void DoSetPosition()
    {
        if (MainManager.Instance.AcceleratorJoystick.Vertical != 0)
        {
            transform.localPosition += MainManager.Instance.AcceleratorJoystick.Vertical * Time.deltaTime * Vector3.forward;
        }
        
        if (MainManager.Instance.PlayerJoystick.Direction.magnitude > 0)
        {
            transform.localPosition += (Vector3)MainManager.Instance.PlayerJoystick.Direction.normalized * (Time.deltaTime * 2);
        }
    }

    public float ScaleFactor()
    {
        return transform.localScale.x;
    }
    
    public void StartGame()
    {
        ChangeState(GameState.Playing);
    }
    
    public void SetPosition()
    {
        if (State == GameState.SetPosition)
        {
            transform.SetParent(null);
            ChangeState(_previousState);
        }
        else
        {
            transform.SetParent(MainManager.Instance.Camera.transform);
            transform.localPosition = new Vector3(0, 0, 6);
            transform.localRotation = new Quaternion(0, 180, 0, 0);
            ChangeState(GameState.SetPosition);
        }
    }
    
    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }
    
    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
    }
    
    public void TogglePause()
    {
        switch (State)
        {
            case GameState.Playing:
                PauseGame();
                break;
            case GameState.Paused:
                ResumeGame();
                break;
            case GameState.Start:
                break;
            case GameState.GameOver:
                break;
            case GameState.SetPosition:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }
    
    void ChangeState(GameState state)
    {
        // if (State == state)
        //     return;
        
        
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Paused:
                MainManager.Pause();
                break;
            case GameState.Playing:
                MainManager.Resume();
                break;
            case GameState.GameOver:
                break;
            case GameState.SetPosition:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        _previousState = State;
        State = state;
        UpdateGameObjects();
    }

    
    void UpdateGameObjects()
    {
        switch (State)
        {
            case GameState.Start:
                SetActive(_activeOnStart, true);
                SetActive(_deactiveOnStart, false);
                break;
            case GameState.Playing:
                SetActive(_activeOnPlaying, true);
                SetActive(_deactiveOnPlaying, false);
                break;
            case GameState.Paused:
                SetActive(_activeOnPaused, true);
                SetActive(_deactiveOnPaused, false);
                break;
            case GameState.GameOver:
                SetActive(_activeOnGameOver, true);
                SetActive(_deactiveOnGameOver, false);
                break;
            case GameState.SetPosition:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    static void SetActive(List<GameObject> gameObjects, bool active)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(active);
        }
    }

    public void Restart()
    {
        MainManager.Instance.Restart();
    }
}


public enum GameState
{
    Start,
    Playing,
    Paused,
    GameOver,
    SetPosition
}