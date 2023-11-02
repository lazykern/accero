using System.Collections;
using UnityEngine;

public class Utils: MonoBehaviour
{
    // ReSharper disable Unity.PerformanceAnalysis
    public static IEnumerator MoveToLocationAndDestroy(Component component, Vector3 destroyLocation, float t = 0.2f)
    {
        float startTime = Time.time;
        var startPosition = component.transform.position;
        while (Time.time < startTime + t)
        {
            component.transform.position = Vector3.Lerp(startPosition, destroyLocation, (Time.time - startTime) / t);
            yield return null;
        }
        Destroy(component.gameObject);
    }
}
