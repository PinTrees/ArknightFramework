using UnityEngine;

public static class TextureEx 
{
    public static Texture2D CropTexture(this Texture2D original, Vector2 ratio)
    {
        int width = original.width;
        int height = original.height;

        float aspectRatio = (float)ratio.x / ratio.y;

        int newWidth = width;
        int newHeight = (int)(width / aspectRatio);

        if (newHeight > height)
        {
            newHeight = height;
            newWidth = (int)(height * aspectRatio);
        }

        int startX = (width - newWidth) / 2;
        int startY = (height - newHeight) / 2;

        Color[] pixels = original.GetPixels(startX, startY, newWidth, newHeight);

        Texture2D croppedTexture = new Texture2D(newWidth, newHeight);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }
}
