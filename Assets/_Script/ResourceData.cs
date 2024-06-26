using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceData
{
    public string key;
    public string path;
    public string downloadUrl;
}

[System.Serializable]
public class ResourceTable
{
    public Dictionary<string, ResourceData> resources;

    public static ResourceTable FromJson(string jsonText)
    {
        ResourceTable resourceTable = new();
        resourceTable.resources = SaveSystem.LoadJson_String<Dictionary<string, ResourceData>>(jsonText);
        return resourceTable;
    }
}