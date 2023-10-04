using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    float handleRange = 1;

    [SerializeField]
    float deadZone;

    [SerializeField]
    AxisOptions axisOptions = AxisOptions.Both;

    [SerializeField]
    bool snapX;

    [SerializeField]
    bool snapY;

    [SerializeField]
    protected RectTransform background;

    [SerializeField]
    RectTransform handle;

    RectTransform baseRect;

    Camera cam;

    Canvas canvas;

    Vector2 input = Vector2.zero;

    float Horizontal
    {
        get => snapX ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x;
    }

    public float Vertical
    {
        get => snapY ? SnapFloat(input.y, AxisOptions.Vertical) : input.y;
    }

    public Vector2 Direction
    {
        get => new Vector2(Horizontal, Vertical);
    }

    public float HandleRange
    {
        get => handleRange;
        private set => handleRange = Mathf.Abs(value);
    }

    public float DeadZone
    {
        get => deadZone;
        set => deadZone = Mathf.Abs(value);
    }

    public bool IsOnTouch { get; private set; }

    public bool IsOnDrag { get; private set; }

    public AxisOptions AxisOptions
    {
        get => AxisOptions;
        set => axisOptions = value;
    }

    public bool SnapX
    {
        get => snapX;
        set => snapX = value;
    }

    public bool SnapY
    {
        get => snapY;
        set => snapY = value;
    }

    protected virtual void Start()
    {
        HandleRange = handleRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        var center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        IsOnDrag = true;

        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        var position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        var radius = background.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        handle.anchoredPosition = input * radius * handleRange;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        IsOnTouch = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        IsOnTouch = false;
        IsOnDrag = false;

        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }

    void FormatInput()
    {
        input = axisOptions switch
        {
            AxisOptions.Horizontal => new Vector2(input.x, 0f),
            AxisOptions.Vertical => new Vector2(0f, input.y),
            _ => input
        };
    }

    float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(input, Vector2.up);
            return snapAxis switch
            {
                AxisOptions.Horizontal when angle is < 22.5f or > 157.5f => 0,
                AxisOptions.Horizontal => value > 0 ? 1 : -1,
                AxisOptions.Vertical when angle is > 67.5f and < 112.5f => 0,
                AxisOptions.Vertical => value > 0 ? 1 : -1,
                _ => value
            };
        }
        switch (value)
        {
            case > 0:
                return 1;
            case < 0:
                return -1;
        }
        return 0;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        var localPoint = Vector2.zero;

        if (!cam && canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            var pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - background.anchorMax * baseRect.sizeDelta + pivotOffset;
        }
        return Vector2.zero;
    }
}

public enum AxisOptions
{
    Both,

    Horizontal,

    Vertical
}
