using UnityEngine;


public static class ComponentEx 
{
    public static T GetOrGetChildrenComponent<T>(this Component component) where T : Component
    {
        T foundComponent = component.GetComponent<T>();

        if (foundComponent != null)
        {
            return foundComponent;
        }

        return component.GetComponentInChildren<T>();
    }

    public static T GetAndGetChildtenAndGetParentComponent<T>(this Component component) where T : Component
    {
        T foundComponent = component.GetComponent<T>();

        if (foundComponent != null)
        {
            return foundComponent;
        }

        foundComponent = component.GetComponentInChildren<T>();

        if (foundComponent != null)
        {
            return foundComponent;
        }

        return component.GetComponentInParent<T>();
    }
}
