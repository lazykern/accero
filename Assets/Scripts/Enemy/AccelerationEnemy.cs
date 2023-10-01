using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AccelerationEnemy : Enemy
{
    
    [SerializeField] float acceleration = 1f;
    
    void Awake()
    {
        base.Awake();
    }
    
    void Update()
    {
        base.Update();
    }
    
    void FixedUpdate()
    {
        // Pull player if player is in range
        var player = Player.Instance;
        var direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * (acceleration * GameManager.Instance.ScaleFactor()) , ForceMode.Acceleration);
    }
}
