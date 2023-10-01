using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityEnemy : Enemy
{
    [SerializeField] float _maxVelocity = 5f;
    [SerializeField] float _acceleration = 5f;
    
    void Awake()
    {
        base.Awake();
    }
    
    void Update()
    {
        base.Update();
        
        var player = Player.Instance;
        var direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * (_acceleration * GameManager.Instance.ScaleFactor()) , ForceMode.Acceleration);
        
        if (rb.velocity.magnitude > _maxVelocity * GameManager.Instance.ScaleFactor())
        {
            rb.velocity = rb.velocity.normalized * (_maxVelocity * GameManager.Instance.ScaleFactor());
        }
    }
}
