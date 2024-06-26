using System.IO;
using UnityEngine;

public class FileEx
{
    public static Texture2D GetPNG(string relativePath)
    {
        if (relativePath == null)
            return null;

        string fullPath = Path.Combine(Application.persistentDataPath, relativePath);

        if (!File.Exists(fullPath))
        {
            return null;
        }


        FileInfo fileInfo = new FileInfo(fullPath);

        if (fileInfo.Exists)
        {
            try
            {
                byte[] byteTexture = System.IO.File.ReadAllBytes(fullPath);
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);
                return texture;
            }
            catch
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
