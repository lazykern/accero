using System.Collections;
using UnityEngine;

public class Utils: MonoBehaviour
{
    public static IEnumerator DestroyItem(Component component, Vector3 destroyLocation)
    {
        var direction = (destroyLocation - component.transform.position).normalized;
        float distance = Vector3.Distance(destroyLocation, component.transform.position);
        float startTime = Time.time;
        
        while (distance > 0.2f)
        {
            if (Time.time - startTime > 2f)
            {
                break;
            }
            component.transform.position += direction * (Time.deltaTime * 10f);
            distance = Vector3.Distance(destroyLocation, component.transform.position);
            yield return null;
        }
        
        Destroy(component.gameObject);
    }
}
