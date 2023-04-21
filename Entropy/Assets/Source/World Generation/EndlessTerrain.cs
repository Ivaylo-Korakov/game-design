using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.World_Generation
{
    public class EndlessTerrain : MonoBehaviour
    {
        public const float MaxViewDist = 400;
        public Transform Viewer;

        public static Vector2 ViewerPosition;
        int _chunkSize;
        int _chunksVisibleInViewDist;
        Dictionary<Vector2,TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
        List<TerrainChunk> visibleLastUpdate = new List<TerrainChunk>();
        
        void Start()
        {
            _chunkSize = MapGenerator.MapChunkSize - 1;
            _chunksVisibleInViewDist = Mathf.RoundToInt(MaxViewDist / _chunkSize);
        }

        void Update()
        {
            ViewerPosition = new Vector2(Viewer.position.x, Viewer.position.z);
            UpdateVisibleChuncks();
        }

        void UpdateVisibleChuncks()
        {
            foreach (var terrainChunk in visibleLastUpdate)
            {
                terrainChunk.SetVisible(false);
            }
            visibleLastUpdate.Clear();

            int currentChunkCordX = Mathf.RoundToInt(ViewerPosition.x / _chunkSize);
            int currentChunkCordY = Mathf.RoundToInt(ViewerPosition.y / _chunkSize);

            for (int yOffest = -_chunksVisibleInViewDist; yOffest <= _chunksVisibleInViewDist; yOffest++)
            {
                for (int xOffest = -_chunksVisibleInViewDist; xOffest < _chunksVisibleInViewDist; xOffest++)
                {
                    Vector2 viewedChuckCord = new Vector2(currentChunkCordX + xOffest,currentChunkCordY + yOffest);

                    if (terrainChunkDictionary.ContainsKey(viewedChuckCord))
                    {
                        terrainChunkDictionary[viewedChuckCord].UpdateTerrainChuk();
                        if (terrainChunkDictionary[viewedChuckCord].IsVisible())
                        {
                            visibleLastUpdate.Add(terrainChunkDictionary[viewedChuckCord]);
                        }
                    }
                    else
                    {
                        terrainChunkDictionary.Add(viewedChuckCord,new TerrainChunk(viewedChuckCord,_chunkSize,transform));
                    }
                }
            }
        }

        public class TerrainChunk
        {
            public GameObject MeshObject;
            public Vector2 Position;
            public Bounds Bounds;

            public TerrainChunk(Vector2 cord, int size,Transform parrent)
            {
                Position = cord * size;
                Bounds = new Bounds(Position, Vector2.one * size);
                Vector3 positionV3 = new Vector3(Position.x, 0, Position.y);

                MeshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
                MeshObject.transform.position = positionV3;
                MeshObject.transform.localScale = Vector3.one * size / 10f;
                MeshObject.transform.parent = parrent;

                SetVisible(false);
            }

            public void UpdateTerrainChuk()
            {
                float viewerDistanceFromEdge = Mathf.Sqrt(Bounds.SqrDistance(ViewerPosition));
                bool visible = viewerDistanceFromEdge <= MaxViewDist;

                SetVisible(visible);
            }

            public void SetVisible(bool visible)
            {
                MeshObject.SetActive(visible);
            }

            public bool IsVisible()
            {
                return MeshObject.activeSelf;
            }
        }
    }
}