using System;
using UnityEngine;

namespace Assets.Source.World_Generation
{
    public class MapDisplay : MonoBehaviour
    {
        public Renderer TextureRenderer;
        // For the mesh
        public MeshFilter MeshFilter;
        public MeshRenderer MeshRenderer;
        
        public void DrawTexture(Texture2D texture)
        {
            // Set the texture of the plane as the texture that has been given to as
            TextureRenderer.sharedMaterial.mainTexture = texture;

            try
            {
                TextureRenderer.material.mainTexture = texture;
            }
            catch (Exception e)
            {
                Debug.Log("Didn't set texture for mainMaterial because you are in edit mode.");
                throw e;
            }
            // Set the size of the plane the same as the texture
            TextureRenderer.transform.localScale = new Vector3(texture.width, 1,texture.height);
        }

        public void DrawMesh(MeshData meshData,Texture2D texture)
        {
            MeshFilter.sharedMesh = meshData.CreateMesh();
            MeshRenderer.sharedMaterial.mainTexture = texture;

            try
            {
                MeshRenderer.material.mainTexture = texture;
            }
            catch (Exception e)
            {
                Debug.Log("Didn't set texture for mainMaterial because you are in edit mode.");
            }
        }
    }
}
