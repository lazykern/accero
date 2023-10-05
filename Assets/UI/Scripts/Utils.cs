using System.Collections;
using UnityEngine;

public class Utils: MonoBehaviour
{
    public static IEnumerator DestroyItem(Component component, Vector3 destroyLocation)
    {
        var direction = (destroyLocation - component.transform.position).normalized;
        float distance = Vector3.Distance(destroyLocation, component.transform.position);
        
        while (distance > 0.1f)
        {
            component.transform.position += direction * (Time.deltaTime * 10f);
            distance = Vector3.Distance(destroyLocation, component.transform.position);
            yield return null;
        }
        
        Destroy(component.gameObject);
    }
}
