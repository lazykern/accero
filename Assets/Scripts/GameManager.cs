using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    Camera _camera;

    void Awake()
    {
        Instance = this;
    }
    
    public float ScaleFactor()
    {
        return transform.localScale.x;
    }
}
