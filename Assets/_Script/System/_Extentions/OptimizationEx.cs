using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class OprimisationMeshContainer
{
    public GameObject gameObject;
    public Mesh mesh;
}

public static class OptimizationEx
{
    public static OprimisationMeshContainer CombineLODGroups(GameObject container, int groupIndex)
    {
        Vector3 initPosition = container.transform.position;
        container.transform.position = Vector3.zero;

        LODGroup[] lODGroups = container.GetComponentsInChildren<LODGroup>();
        List<MeshFilter> meshFilters = new();

        foreach(var lodGroup in lODGroups)
        {
            LOD[] lods = lodGroup.GetLODs();
            LOD lod = lods[lods.Length - (groupIndex + 1)];

            foreach(var renderer in lod.renderers)
            {
                MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();
                meshFilters.Add(meshFilter);
            }
        }

        Dictionary<Material, List<CombineInstance>> materialToMeshCombines = new Dictionary<Material, List<CombineInstance>>();

        // Group meshes by material
        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();
            if (meshRenderer == null || meshFilter.sharedMesh == null)
                continue;

            Material material = meshRenderer.sharedMaterial;
            if (!materialToMeshCombines.ContainsKey(material))
            {
                materialToMeshCombines[material] = new List<CombineInstance>();
            }

            CombineInstance combineInstance = new CombineInstance();
            combineInstance.mesh = meshFilter.sharedMesh;
            combineInstance.transform = meshFilter.transform.localToWorldMatrix;
            materialToMeshCombines[material].Add(combineInstance);
        }


        List<Material> materials = new List<Material>();
        List<CombineInstance> finalCombineInstances = new List<CombineInstance>();

        // Combine meshes for each material
        foreach (var kvp in materialToMeshCombines)
        {
            Material material = kvp.Key;
            List<CombineInstance> combines = kvp.Value;

            Mesh subMesh = new Mesh();
            subMesh.CombineMeshes(combines.ToArray(), true, true);

            CombineInstance finalCombineInstance = new CombineInstance();
            finalCombineInstance.mesh = subMesh;
            finalCombineInstance.transform = Matrix4x4.identity;
            finalCombineInstances.Add(finalCombineInstance);

            materials.Add(material);
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(finalCombineInstances.ToArray(), false, false);

        // Create a new GameObject for the combined mesh
        GameObject combinedObject = new GameObject(container.name + "_Combined");
        MeshFilter mf = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer mr = combinedObject.AddComponent<MeshRenderer>();

        mf.mesh = combinedMesh;
        combinedObject.transform.position = initPosition;
        mr.sharedMaterials = materials.ToArray();

        container.transform.position = initPosition;
        return new OprimisationMeshContainer()
        {
            gameObject = combinedObject,
            mesh = combinedMesh,
        };
    }

    // Method to generate a unique asset path
    private static string GetUniqueAssetPath(string directoryPath, string baseName, string extension)
    {
        string path;
        int count = 0;
        do
        {
            string fileName = baseName + (count > 0 ? "_" + count.ToString() : "") + extension;
            path = Path.Combine(directoryPath, fileName);
            count++;
        } while (File.Exists(path));

        return path;
    }
}
