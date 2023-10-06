using System.Collections;
using UnityEngine;

public class Utils: MonoBehaviour
{
    public static IEnumerator MoveToLocationAndDestroy(Component component, Vector3 destroyLocation)
    {
        while (component.transform.position != destroyLocation)
        {
            component.transform.position = Vector3.MoveTowards(component.transform.position, destroyLocation, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        Destroy(component.gameObject);
    }
}
