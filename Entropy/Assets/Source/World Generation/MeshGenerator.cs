using UnityEngine;

namespace Assets.Source.World_Generation
{
    public static class MeshGenerator
    {
        public static MeshData GenerateTerrainMesh(float[,] heigthMap, float heigthMultiplier,AnimationCurve heigthCurve,int levelOfDetail)
        {
            int width = heigthMap.GetLength(0);
            int heigth = heigthMap.GetLength(1);
            float topLeftX = (width - 1) / -2f;
            float topLeftZ = (heigth - 1) / 2f;

            int meshSimplificationIncroment = (levelOfDetail * 2) == 0 ? 1 : levelOfDetail * 2;
            int verticesPerLine = (width - 1) / meshSimplificationIncroment + 1;

            MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
            int vertexIndex = 0;

            for (int y = 0; y < heigth; y += meshSimplificationIncroment)
            {
                for (int x = 0; x < width; x += meshSimplificationIncroment)
                {
                    meshData.Vertices[vertexIndex] = new Vector3(topLeftX + x,heigthCurve.Evaluate(heigthMap[x, y])  * heigthMultiplier, topLeftZ - y);
                    meshData.Uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)heigth);
                   // meshData.Uvs[vertexIndex] = new Vector2(0,0);

                    if (x < width - 1 && y < heigth - 1)
                    {
                        meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                        meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                    }

                    vertexIndex++;
                }
            }

            return meshData;
        }
    }

    public class MeshData
    {
        public Vector3[] Vertices;
        public int[] Triangles;
        public Vector2[] Uvs;

        public int TriangleIndex;

        public MeshData(int meshWidth, int meshHeigth)
        {
            Vertices = new Vector3[meshWidth * meshHeigth];
            Triangles = new int[(meshWidth - 1) * (meshHeigth - 1) * 6];
            Uvs = new Vector2[meshWidth * meshHeigth];

            TriangleIndex = 0;
        }

        public void AddTriangle(int a, int b, int c)
        {
            Triangles[TriangleIndex] = a;
            Triangles[TriangleIndex + 1] = b;
            Triangles[TriangleIndex + 2] = c;
            
            TriangleIndex += 3;
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = Vertices;
            mesh.triangles = Triangles;
            mesh.uv = Uvs;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
