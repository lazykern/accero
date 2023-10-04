using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class Center : MonoBehaviour
{
    public static Center Instance { get; private set; }


    public Rigidbody rb { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
