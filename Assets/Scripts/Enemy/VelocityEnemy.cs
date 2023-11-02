using UnityEngine;

public class VelocityEnemy : Enemy
{
    [SerializeField]
    float _maxVelocity = 5f;

    [SerializeField]
    float _acceleration = 5f;

    new void Awake()
    {
        base.Awake();
    }

    new void Update()
    {
        base.Update();

        if (rb.velocity.magnitude > _maxVelocity * GameManager.Instance.ScaleFactor())
        {
            rb.velocity = rb.velocity.normalized * (_maxVelocity * GameManager.Instance.ScaleFactor());
        }
    }
    
    void FixedUpdate()
    {
        if (!_isAlive)
            return;
        var player = PlayerController.Instance.Player;
        var direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * (_acceleration * GameManager.Instance.ScaleFactor()), ForceMode.Acceleration);
    }
}
