using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public float ScaleFactor()
    {
        return transform.localScale.x;
    }
}
