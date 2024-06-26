using UnityEngine;
using UnityEngine.UIElements;

public struct TerrainInfo
{
    public float slope;
    public Vector3 position;
    public Vector3 normal;
}

public static class TerrainEx
{
    public static TerrainInfo? GetTerrainInfo(float positionX, float positionZ)
    {
        TerrainInfo info = new TerrainInfo();

        // ��� Terrain�� �ݺ��Ͽ� �ش� ��ġ�� �����ϴ��� Ȯ��
        foreach (Terrain terrain in Terrain.activeTerrains)
        {
            TerrainData terrainData = terrain.terrainData;
            Vector3 terrainPosition = terrain.transform.position;
            float terrainWidth = terrainData.size.x;
            float terrainHeight = terrainData.size.z;

            // �ش� ��ġ�� Terrain ���� ���� �ִ��� Ȯ��
            if (positionX >= terrainPosition.x && positionX <= terrainPosition.x + terrainWidth &&
                positionZ >= terrainPosition.z && positionZ <= terrainPosition.z + terrainHeight)
            {
                // Terrain �����Ϳ��� ��ġ ������ ������
                float relativeX = (positionX - terrainPosition.x) / terrainWidth;
                float relativeZ = (positionZ - terrainPosition.z) / terrainHeight;
                float height = terrain.SampleHeight(new Vector3(positionX, 0, positionZ)) + terrainPosition.y;
                Vector3 position = new Vector3(positionX, height, positionZ);

                // Terrain ��� ���͸� ������
                Vector3 normal = terrainData.GetInterpolatedNormal(relativeX, relativeZ);

                // ��絵�� ���
                float slope = terrainData.GetSteepness(relativeX, relativeZ);

                // TerrainInfo ����ü�� ä��
                info.position = position;
                info.normal = normal;
                info.slope = slope;

                return info;
            }
        }

        //Debug.LogWarning("Position is outside the bounds of all terrains.");
        return null;
    }
}
