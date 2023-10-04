using UnityEngine;

public class AcceroController : MonoBehaviour
{
    [SerializeField]
    float _maxCentripetalAcceleration = 100f;

    [SerializeField]
    float _minCentripetalAcceleration = 0.1f;

    [SerializeField]
    float _initialCentripetalAcceleration = 20f;

    [SerializeField]
    Joystick _joystick;

    float centripetalAcceleration;

    void Start()
    {
        centripetalAcceleration = _initialCentripetalAcceleration;
    }

    void Update()
    {
        if (!_joystick.IsOnTouch)
        {
            return;
        }

        float joystickPercent = (_joystick.Vertical + _joystick.HandleRange) / (2 * _joystick.HandleRange);
        centripetalAcceleration = joystickPercent < 0.5f
            ? Mathf.Lerp(_minCentripetalAcceleration, _initialCentripetalAcceleration, joystickPercent / 0.5f)
            : Mathf.Lerp(_initialCentripetalAcceleration, _maxCentripetalAcceleration, (joystickPercent - 0.5f) * 2);
    }

    void FixedUpdate()
    {
        if (!_joystick.IsOnTouch)
        {
            return;
        }

        var delta = Center.Instance.transform.position - Player.Instance.transform.position;
        var direction = delta.normalized;
        var force = direction * (centripetalAcceleration * GameManager.Instance.ScaleFactor());

        Player.Instance.rb.AddForce(force, ForceMode.Acceleration);
    }
}
