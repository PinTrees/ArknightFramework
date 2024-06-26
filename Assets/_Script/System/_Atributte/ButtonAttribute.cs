using System;
using UnityEngine;
using System.Reflection;


#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ButtonAttribute : PropertyAttribute
{
    public string buttonText;

    public ButtonAttribute(string buttonText = null)
    {
        this.buttonText = buttonText;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // ��� ��ü�� Ÿ�� ��������
        Type targetType = target.GetType();

        // �ش� ��ü�� ��� �޼��带 ��������
        MethodInfo[] methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        // �� �޼��忡 ����
        foreach (var method in methods)
        {
            // ButtonAttribute�� ����� �޼��� ã��
            var buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
            if (buttonAttribute != null)
            {
                // ��ư �ؽ�Ʈ ����
                string buttonText = string.IsNullOrEmpty(buttonAttribute.buttonText) ? method.Name : buttonAttribute.buttonText;

                // ��ư �׸���
                if (GUILayout.Button(buttonText))
                {
                    // �޼��� ����
                    method.Invoke(target, null);
                }
            }
        }
    }
}
#endif