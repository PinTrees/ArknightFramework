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
    /// 객체를 JSON 형식으로 직렬화하여 지정된 경로에 저장합니다.
    /// </summary>
    /// <param name="obj">저장할 객체</param>
    /// <param name="relativePath">저장할 파일의 Assets 폴더 상대 경로 (예: "Resources/MyFile")</param>
    /// <param name="extension">파일 확장자 (기본값은 ".json")</param>
    /// <param name="formatting">JSON 포맷팅 옵션 (기본값은 Formatting.Indented)</param>
    /// <param name="encoding">파일 인코딩 (기본값은 UTF-8)</param>
    public static void SaveJson_AssetPath(object obj, string relativePath, string extension = ".json", Formatting formatting = Formatting.Indented, System.Text.Encoding encoding = null)
    {
        if (encoding == null)
            encoding = System.Text.Encoding.UTF8;

        // JSON으로 객체 직렬화
        string json = JsonConvert.SerializeObject(obj, formatting);

        // 파일 확장자 확인 및 추가
        string fullPath = Path.Combine(Application.dataPath, relativePath);
        if (!Path.GetExtension(fullPath).Equals(extension, StringComparison.OrdinalIgnoreCase))
            fullPath = $"{fullPath}{extension}";

        // 디렉토리 확인 및 생성
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            // 파일로 JSON 문자열 저장
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
        // 파일 확장자 확인 및 추가
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
