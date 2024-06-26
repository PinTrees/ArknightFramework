using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public static class TransformEx 
{
    public static void SetLayerAll(this Transform transform, int layer)
    {
        if (transform == null)
        {
            return;
        }

        transform.gameObject.layer = layer;

        foreach (Transform child in transform)
        {
            SetLayerAll(child, layer);
        }
    }

    public static void SetZeroLocalPositonAndRotation(this Transform transform)
    {
        if (transform == null) return;

        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public static void SetTransformForTarget(Transform parent, Transform child, Transform target)
    {
        parent.SetParent(target, true);
        parent.SetZeroLocalPositonAndRotation();

        var dir = (child.position - parent.position).normalized;
        var dit = Vector3.Distance(parent.position, child.position);

        parent.position -= dir * dit; 
    }

    public static void LookCameraY(this Transform transform, float rotationSpeed)
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public static void LookAtY(this Transform transform, Transform target, float rotationSpeed)
    {
        if (target == null)
        {
            return;
        }

        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0; // Ignore y component to keep the rotation only in the horizontal plane

        Quaternion targetRotation = Quaternion.LookRotation(lookPos);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Create a new Quaternion maintaining only the y-axis rotation

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public static void LookAtOnlyY(this Transform transform, Transform target, float rotationSpeed)
    {
        if (target == null)
        {
            return;
        }

        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0; // Ignore y component to keep the rotation only in the horizontal plane

        Quaternion targetRotation = Quaternion.LookRotation(lookPos);
        targetRotation = Quaternion.Euler(transform.eulerAngles.x,
            targetRotation.eulerAngles.y, transform.eulerAngles.z); // Create a new Quaternion maintaining only the y-axis rotation

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public static void LookAtPositionOnlyY(this Transform transform, Vector3 lookPosisiton, float rotationSpeed)
    {
        Vector3 direction = new Vector3(lookPosisiton.x - transform.position.x, 0, lookPosisiton.z - transform.position.z);

        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public static void LookAtY(this Transform transform, Transform target)
    {
        if (target == null)
        {
            return;
        }

        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0; // Ignore y component to keep the rotation only in the horizontal plane

        Quaternion targetRotation = Quaternion.LookRotation(lookPos);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Create a new Quaternion maintaining only the y-axis rotation

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100);
    }
}
