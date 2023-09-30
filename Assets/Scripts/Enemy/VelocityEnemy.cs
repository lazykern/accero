using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityEnemy : Enemy
{
    [SerializeField] float velocity = 1f;
    
    private float _lossyVelocity;

    void Awake()
    {
        base.Awake();
        _lossyVelocity = velocity * transform.lossyScale.x;
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
        rb.velocity = direction.normalized * _lossyVelocity;
    }
}
