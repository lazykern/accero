using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityEnemy : Enemy
{
    [SerializeField] float velocity = 5f;
    
    private float _lossyVelocity;

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
        rb.velocity = direction.normalized * (velocity * GameManager.Instance.ScaleFactor());
    }
}
