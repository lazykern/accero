using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
