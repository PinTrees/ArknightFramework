using UnityEngine;

public class ColorEx 
{
    public static Color FromHex(int hexValue)
    {
        float r = ((hexValue >> 16) & 0xFF) / 255f;
        float g = ((hexValue >> 8) & 0xFF) / 255f;
        float b = (hexValue & 0xFF) / 255f;
        return new Color(r, g, b);
    }
}
