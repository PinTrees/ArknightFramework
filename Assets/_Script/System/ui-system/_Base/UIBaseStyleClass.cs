using UnityEngine;

public enum MainAxisAlignment
{
    Start,
    Center,
    End,
}

public enum CrossAxisAlignment
{
    Start,
    Center,
    End,
}

public enum FlexFit
{
    Tight,
    Fixed,
    Loose,
}

public enum Alignment
{
    Center,
    BottomCenter,
    BottomLeft,
    BottomRight,
    CenterLeft,
    CenterRight,
    TopCenter,
    TopLeft,
    TopRight,
}

public enum Axis
{
    All,
    Row,
    Column,
}

[System.Serializable]
public struct EdgeInsetsData
{
    public float Left;
    public float Top;
    public float Right;
    public float Bottom;

    public bool IsZero()
    {
        return Left == 0 && Top == 0 && Right == 0 && Bottom == 0;
    }
}




namespace Flutter
{
    
}