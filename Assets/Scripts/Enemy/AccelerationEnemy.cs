using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AccelerationEnemy : Enemy
{
    
    [SerializeField] float acceleration = 5f;
    
    private float _lossyAcceleration;

    void Awake()
    {
        base.Awake();
        _lossyAcceleration = acceleration * transform.lossyScale.x;
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
        
        rb.AddForce(direction.normalized * _lossyAcceleration, ForceMode.Acceleration);
    }
}
