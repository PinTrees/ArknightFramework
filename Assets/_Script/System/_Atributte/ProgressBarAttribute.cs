using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProgressBarAttribute : PropertyAttribute
{
    public bool zeroDisable;

    public ProgressBarAttribute(bool zeroDisable = false)
    {
        this.zeroDisable = zeroDisable;
    }
}


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ProgressBarAttribute))]
public class ProgressBarDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ProgressBarAttribute progressBar = (ProgressBarAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.Float)
        {
            float value = property.floatValue;
            if (progressBar.zeroDisable && Mathf.Approximately(value, 0f))
            {
                EditorGUI.LabelField(position, label.text, "0%");
            }
            else
            {
                Rect rect = EditorGUI.PrefixLabel(position, label);
                EditorGUI.ProgressBar(rect, value, $"{value * 100f}%");
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use ProgressBar with float.");
        }
    }
}
#endif