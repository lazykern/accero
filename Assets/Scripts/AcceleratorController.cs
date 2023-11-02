using UnityEngine;

public class AcceleratorController : MonoBehaviour
{
    [SerializeField]
    float _maxCentripetalAcceleration = 100f;

    [SerializeField]
    float _minCentripetalAcceleration = -50;

    [SerializeField]
    float _initialCentripetalAcceleration = 25;

    [SerializeField]
    LineRenderer _accelerationLine;
    
    [SerializeField]
    LineRenderer _trajectoryLine;
    
    float centripetalAcceleration;

    void Start()
    {
        centripetalAcceleration = _initialCentripetalAcceleration;
    }

    void Update()
    {
        if (GameManager.Instance.State != GameState.Playing)
        {
            _accelerationLine.enabled = false;
            _trajectoryLine.enabled = false;
            return;
        }
        
        if (!MainManager.Instance.AcceleratorJoystick.IsOnTouch)
        {
            _accelerationLine.enabled = false;
            _trajectoryLine.enabled = false;
            return;
        }

        _accelerationLine.enabled = true;
        
        if (MainManager.trajectoryEnabled)
            _trajectoryLine.enabled = true;

        float joystickPercent = (MainManager.Instance.AcceleratorJoystick.Vertical + MainManager.Instance.AcceleratorJoystick.HandleRange) / (2 * MainManager.Instance.AcceleratorJoystick.HandleRange);
        centripetalAcceleration = joystickPercent < 0.5f
            ? Mathf.Lerp(_minCentripetalAcceleration, _initialCentripetalAcceleration, joystickPercent / 0.5f)
            : Mathf.Lerp(_initialCentripetalAcceleration, _maxCentripetalAcceleration, (joystickPercent - 0.5f) * 2);
        
        UpdateAcceroLine();
        
        if (MainManager.trajectoryEnabled)
            UpdateTrajectoryLine();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.State != GameState.Playing)
            return;
        
        if (!MainManager.Instance.AcceleratorJoystick.IsOnTouch)
        {
            return;
        }

        var delta = transform.position - PlayerController.Instance.Player.transform.position;
        var direction = delta.normalized;
        var acceleration = direction * (centripetalAcceleration * GameManager.Instance.ScaleFactor());

        PlayerController.Instance.Player.rb.AddForce(acceleration, ForceMode.Acceleration);
    }

    void UpdateAcceroLine()
    {
        _accelerationLine.SetPosition(0, transform.position);
        _accelerationLine.SetPosition(1, PlayerController.Instance.Player.transform.position);

        var color = centripetalAcceleration switch
        {
            > 0 => Color.Lerp(Color.white, Color.red, centripetalAcceleration / _maxCentripetalAcceleration),
            < 0 => Color.Lerp(Color.white, Color.green, centripetalAcceleration / _minCentripetalAcceleration),
            _ => Color.white
        };
        _accelerationLine.startColor = color;
        _accelerationLine.endColor = color;
    }

    void UpdateTrajectoryLine()
    {
        var positions = new Vector3[MainManager.trajectoryLength];
        positions[0] = PlayerController.Instance.Player.transform.position;
        
        var velocity = PlayerController.Instance.Player.rb.velocity;
        int i;
        
        for (i = 1; i < positions.Length; i++)
        {
            var delta = transform.position - positions[i - 1];
            var direction = delta.normalized;
            var acceleration = direction * (centripetalAcceleration * GameManager.Instance.ScaleFactor());
            var position = positions[i - 1] + velocity * Time.fixedDeltaTime + acceleration * (Time.fixedDeltaTime * Time.fixedDeltaTime) / 2;
            
            if (!GameManager.Instance.GameArea.bounds.Contains(position))
            {
                break;
            }
            
            positions[i] = position;
            velocity += acceleration * Time.fixedDeltaTime;
        }
        
        _trajectoryLine.positionCount = i;
        _trajectoryLine.SetPositions(positions);
    }
}
