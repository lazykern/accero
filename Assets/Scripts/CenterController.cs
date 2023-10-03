using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{
    [SerializeField] GameObject _ground;

    Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        UpdateCenter();
    }
    
    void UpdateCenter()
    {
        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (!Physics.Raycast(ray, out var hit) || hit.collider.gameObject != _ground)
            return;

        Center.Instance.transform.position = Vector3.Lerp(Center.Instance.transform.position, hit.point, 0.1f);
        
        var direction = hit.point - Center.Instance.transform.position;
            
        if (direction.magnitude != 0 && direction != Vector3.zero)
        {
            Center.Instance.transform.rotation = Quaternion.Lerp(Center.Instance.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }
}
