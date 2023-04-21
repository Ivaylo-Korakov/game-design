using UnityEngine;

namespace Assets.Source.World_Generation
{
    public enum DrawMode
    {
        NoiseMap,
        ColorMap,
        DrawMesh,
        ColorMap_And_DrawMesh
    }

    public class MapGenerator : MonoBehaviour
    {
        public DrawMode DrawMode;

        public const int MapChunkSize = 241;
        [Range(0, 6)]
        public int LevelOfDetail;
        // Map Dimensions
        public float NoiseScale;

        public int Octaves;
        [Range(0, 1)]
        public float Persistance;
        public float Lacunarity;

        public int Seed;
        public Vector2 Offset;

        public float MeshHeightMultiplier;
        public AnimationCurve MeshHeigthCurve;

        public bool DoesAutoGenerate;

        public TerrainType[] Regions;

        public void DrawMapInEditor()
        {
            MapData mapData = GenerateMapData();

            MapDisplay display = FindObjectOfType<MapDisplay>();

            if (DrawMode == DrawMode.NoiseMap)
            {
                display.DrawTexture(TextureGenerator.TextureFromHeigthMap(mapData.HeigthMap));
            }
            else if (DrawMode == DrawMode.ColorMap)
            {
                display.DrawTexture(TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapChunkSize, MapChunkSize));
            }
            else if (DrawMode == DrawMode.DrawMesh)
            {
                display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.HeigthMap, MeshHeightMultiplier, MeshHeigthCurve, LevelOfDetail), TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapChunkSize, MapChunkSize));
            }
            else if (DrawMode == DrawMode.ColorMap_And_DrawMesh)
            {
                display.DrawTexture(TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapChunkSize, MapChunkSize));
                display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.HeigthMap, MeshHeightMultiplier, MeshHeigthCurve, LevelOfDetail), TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapChunkSize, MapChunkSize));
            }
        }

        MapData GenerateMapData()
        {
            // Fetching the 2d noise map
            var noiseMap = Noise.GenerateNoiseMap(MapChunkSize, MapChunkSize, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);

            Color[] colorMap = new Color[MapChunkSize * MapChunkSize];

            for (int y = 0; y < MapChunkSize; y++)
            {
                for (int x = 0; x < MapChunkSize; x++)
                {
                    float currentHeigth = noiseMap[x, y];
                    for (int i = 0; i < Regions.Length; i++)
                    {
                        if (currentHeigth <= Regions[i].Heigth)
                        {
                            colorMap[y * MapChunkSize + x] = Regions[i].Color;
                            break;
                        }
                    }
                }
            }

            return new MapData(noiseMap,colorMap);
        }

        void OnValidate()
        {
            if (Octaves < 1)
            {
                Octaves = 1;
            }
            if (Lacunarity < 1)
            {
                Lacunarity = 1;
            }
        }
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string Name;
        public float Heigth;
        public Color Color;
    }

    public struct MapData
    {
        public float[,] HeigthMap;
        public Color[] ColorMap;

        public MapData(float[,] heightMap,Color[] colorMap)
        {
            this.HeigthMap = heightMap;
            this.ColorMap = colorMap;
        }
    }
}
