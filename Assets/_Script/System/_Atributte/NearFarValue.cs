using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class MinMaxValue
{
    public float near;
    public float far;
}

public class MinMaxFloatSliderAttribute : PropertyAttribute
{
    public float Min { get; private set; }
    public float Max { get; private set; }

    public MinMaxFloatSliderAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(MinMaxFloatSliderAttribute))]
public class MinMaxFloatAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MinMaxFloatSliderAttribute sliderAttr = attribute as MinMaxFloatSliderAttribute;

        SerializedProperty minProp = property.FindPropertyRelative("min");
        SerializedProperty maxProp = property.FindPropertyRelative("max");

        if (minProp != null && maxProp != null)
        {
            EditorGUI.BeginChangeCheck();
            float minValue = minProp.floatValue;
            float maxValue = maxProp.floatValue;

            EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, sliderAttr.Min, sliderAttr.Max);

            if (EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = minValue;
                maxProp.floatValue = maxValue;
                minProp.serializedObject.ApplyModifiedProperties();
                maxProp.serializedObject.ApplyModifiedProperties();
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "MinMaxRangeSliderAttribute used incorrectly. Fields not found.");
        }
    }
}
#endif
