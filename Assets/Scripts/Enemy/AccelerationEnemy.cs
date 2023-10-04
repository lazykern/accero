using UnityEngine;

public class AccelerationEnemy : Enemy
{
    [SerializeField]
    float acceleration = 5f;

    new void Awake()
    {
        base.Awake();
    }

    new void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        // Pull player if player is in range
        var player = Player.Instance;
        var direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * (acceleration * GameManager.Instance.ScaleFactor()), ForceMode.Acceleration);
    }
}
