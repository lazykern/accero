using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; }

    void Awake()
    {
        Instance = this;
        Instance.State = GameState.Start;
    }
    
    public float ScaleFactor()
    {
        return transform.localScale.x;
    }
    
    public static void Pause()
    {
        Instance.State = GameState.Paused;
        Time.timeScale = 0;
    }
    
    public static void Resume()
    {
        Instance.State = GameState.Playing;
        Time.timeScale = 1;
    }
}


public enum GameState
{
    Start,
    Playing,
    Paused,
    GameOver
}