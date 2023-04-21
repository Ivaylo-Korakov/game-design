using UnityEngine;

namespace Assets.Source.World_Generation
{
    public static class TextureGenerator
    {
        public static Texture2D TextureFromColorMap(Color[] colorMap,int width,int heigth)
        {
            Texture2D texture = new Texture2D(width,heigth);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(colorMap);
            texture.Apply();
            return texture;
        }

        public static Texture2D TextureFromHeigthMap(float[,] heigthMap)
        {
            // Get the size of the map
            int width = heigthMap.GetLength(0);
            int heigth = heigthMap.GetLength(1);
            
            Color[] colorMap = new Color[width * heigth];

            // Get the values for the color map from the noise map
            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int indexForColorMap = x + (y * width);
                    Color colorAtPosition = Color.Lerp(Color.black, Color.white, heigthMap[x, y]);
                    colorMap[indexForColorMap] = colorAtPosition;
                }
            }

            return TextureFromColorMap(colorMap,width,heigth);
        }
    }
}
