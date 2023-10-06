using UnityEngine;

public class AcceleratorController : MonoBehaviour
{
    [SerializeField]
    float _maxCentripetalAcceleration = 100f;

    [SerializeField]
    float _minCentripetalAcceleration;

    [SerializeField]
    float _initialCentripetalAcceleration = 20f;

    [SerializeField]
    LineRenderer _accelerationLine;
    
    float centripetalAcceleration;

    void Start()
    {
        centripetalAcceleration = _initialCentripetalAcceleration;
    }

    void Update()
    {
        if (GameManager.Instance.State != GameState.Playing)
            return;
        
        if (!MainManager.Instance.AcceleratorJoystick.IsOnTouch)
        {
            _accelerationLine.enabled = false;
            return;
        }

        _accelerationLine.enabled = true;

        float joystickPercent = (MainManager.Instance.AcceleratorJoystick.Vertical + MainManager.Instance.AcceleratorJoystick.HandleRange) / (2 * MainManager.Instance.AcceleratorJoystick.HandleRange);
        centripetalAcceleration = joystickPercent < 0.5f
            ? Mathf.Lerp(_minCentripetalAcceleration, _initialCentripetalAcceleration, joystickPercent / 0.5f)
            : Mathf.Lerp(_initialCentripetalAcceleration, _maxCentripetalAcceleration, (joystickPercent - 0.5f) * 2);
        
        UpdateAcceroLine();
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
        var force = direction * (centripetalAcceleration * GameManager.Instance.ScaleFactor());

        PlayerController.Instance.Player.rb.AddForce(force, ForceMode.Acceleration);
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
}
