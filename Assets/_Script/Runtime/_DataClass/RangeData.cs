using System;
using System.Collections.Generic;

[Serializable]
public class GridPosition
{
    public int row;
    public int col;
}

[Serializable]
public class RangeInfo
{
    public string id;
    public int direction;
    public List<GridPosition> grids;
}
