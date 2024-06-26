using UnityEngine;

#if UNITY_ADDRESSABLEASSETS
#if UNITY_EDITOR
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using UnityEditor;
#endif

public class AddressableEx : MonoBehaviour
{
#if UNITY_EDITOR
    public static void SetActiveAddresableFile(string fullPath, bool active=true, string groupName = "Default Local Group", string labelName = "Default Label")
    {
        //Use this object to manipulate addressables
        var settings = AddressableAssetSettingsDefaultObject.Settings;

        string group_name = groupName;
        string label_name = labelName;

        AddressableAssetGroup group = settings.FindGroup(group_name);
        if (group == null)
        {
            group = settings.CreateGroup(group_name, false, false, false, settings.DefaultGroup.Schemas);
        }

        // Remove Group
        //AddressableAssetGroup g = settings.FindGroup(group_name);
        //settings.RemoveGroup(g);

        // Remove a label
        //settings.RemoveLabel(label_name, false);


        // FullPath /Directory/name.file
        var guid = AssetDatabase.AssetPathToGUID(fullPath);

        //This is the function that actually makes the object addressable
        var entry = settings.CreateOrMoveEntry(guid, group);

        // Add Lavel Data
        //entry.labels.Add(label_name);
        entry.address = fullPath;

        //You'll need these to run to save the changes!
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);

        AssetDatabase.SaveAssets();
    }

    // Addressables ºôµå
    public static void BuildAddressables()
    {
        // AddressableAssetSettings °¡Á®¿À±â
        var settings = AddressableAssetSettingsDefaultObject.Settings;

        if (settings == null)
        {
            Debug.LogError("AddressableAssetSettings not found. Make sure Addressables is properly configured in the project.");
            return;
        }

        // Addressables ºôµå ½ÃÀÛ
        Debug.Log("Starting Addressables build...");

        // Addressables ÄÜÅÙÃ÷ ºôµå
        AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult result);

        // ºôµå °á°ú È®ÀÎ
        if (string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Addressables build completed successfully.");
        }
        else
        {
            Debug.LogError($"Addressables build failed: {result.Error}");
        }
    }
#endif
}
#endif