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

        // 모든 Terrain을 반복하여 해당 위치를 포함하는지 확인
        foreach (Terrain terrain in Terrain.activeTerrains)
        {
            TerrainData terrainData = terrain.terrainData;
            Vector3 terrainPosition = terrain.transform.position;
            float terrainWidth = terrainData.size.x;
            float terrainHeight = terrainData.size.z;

            // 해당 위치가 Terrain 범위 내에 있는지 확인
            if (positionX >= terrainPosition.x && positionX <= terrainPosition.x + terrainWidth &&
                positionZ >= terrainPosition.z && positionZ <= terrainPosition.z + terrainHeight)
            {
                // Terrain 데이터에서 위치 정보를 가져옴
                float relativeX = (positionX - terrainPosition.x) / terrainWidth;
                float relativeZ = (positionZ - terrainPosition.z) / terrainHeight;
                float height = terrain.SampleHeight(new Vector3(positionX, 0, positionZ)) + terrainPosition.y;
                Vector3 position = new Vector3(positionX, height, positionZ);

                // Terrain 노멀 벡터를 가져옴
                Vector3 normal = terrainData.GetInterpolatedNormal(relativeX, relativeZ);

                // 경사도를 계산
                float slope = terrainData.GetSteepness(relativeX, relativeZ);

                // TerrainInfo 구조체를 채움
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
