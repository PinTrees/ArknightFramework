using UnityEngine;


public class RaycastFilter : MonoBehaviour, ICanvasRaycastFilter
{
    public bool isRaycastTarget = true;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRaycastTarget;
    }
}