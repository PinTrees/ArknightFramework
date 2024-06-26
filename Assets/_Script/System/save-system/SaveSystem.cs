using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class SaveSystem : MonoBehaviour
{
#if UNITY_EDITOR
    /// <summary>
    /// ��ü�� JSON �������� ����ȭ�Ͽ� ������ ��ο� �����մϴ�.
    /// </summary>
    /// <param name="obj">������ ��ü</param>
    /// <param name="relativePath">������ ������ Assets ���� ��� ��� (��: "Resources/MyFile")</param>
    /// <param name="extension">���� Ȯ���� (�⺻���� ".json")</param>
    /// <param name="formatting">JSON ������ �ɼ� (�⺻���� Formatting.Indented)</param>
    /// <param name="encoding">���� ���ڵ� (�⺻���� UTF-8)</param>
    public static void SaveJson_AssetPath(object obj, string relativePath, string extension = ".json", Formatting formatting = Formatting.Indented, System.Text.Encoding encoding = null)
    {
        if (encoding == null)
            encoding = System.Text.Encoding.UTF8;

        // JSON���� ��ü ����ȭ
        string json = JsonConvert.SerializeObject(obj, formatting);

        // ���� Ȯ���� Ȯ�� �� �߰�
        string fullPath = Path.Combine(Application.dataPath, relativePath);
        if (!Path.GetExtension(fullPath).Equals(extension, StringComparison.OrdinalIgnoreCase))
            fullPath = $"{fullPath}{extension}";

        // ���丮 Ȯ�� �� ����
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            // ���Ϸ� JSON ���ڿ� ����
            File.WriteAllText(fullPath, json, encoding);
            Debug.Log($"JSON saved to: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving JSON to {fullPath}: {e.Message}");
        }

        AssetDatabase.Refresh();
    }
#endif

#if UNITY_EDITOR
    public static T LoadJson_AssetPath<T>(string relativePath, string extension = ".json") where T : class
    {
        // ���� Ȯ���� Ȯ�� �� �߰�
        string fullPath = Path.Combine(Application.dataPath, relativePath);
        if (!Path.GetExtension(fullPath).Equals(extension, StringComparison.OrdinalIgnoreCase))
            fullPath = $"{fullPath}{extension}";

        if (!File.Exists(fullPath))
        {
            Debug.LogError($"File not found at path: {fullPath}");
            return null;
        }

        try
        {
            string json = File.ReadAllText(fullPath);
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error reading JSON file at path: {fullPath}. Exception: {e.Message}");
            return null;
        }
    }
#endif

    public static T LoadJson_String<T>(string jsonString) where T : class
    {
        if (jsonString == null)
            return null;
        if (jsonString == "")
            return null;

        T result = JsonConvert.DeserializeObject<T>(jsonString);
        return result;
    }

    public static void SaveJson(object obj, string path)
    {

    }

    public static void LoadJson<T>(string path)
    {

    }

#if UNITY_NEWTONSOFT_JSON
#endif
}
