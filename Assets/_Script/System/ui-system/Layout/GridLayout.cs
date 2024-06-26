using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class GridLayout : UILayoutBase
{
    public List<RectTransform> childRectTransforms = new();

    public Vector2 spacing = Vector2.zero;
    public Vector2 fixedGridSize = Vector2.zero;

    public FlexFit rowAxisFit = FlexFit.Fixed;
    public FlexFit columnAxisFit = FlexFit.Fixed;

    public int fixedColumnCount = 0;


    private void Reset()
    {
        Init();
        SetLayout();
    }

    public override void SetLayout()
    {
        base.SetLayout();

        childRectTransforms = target.GetComponentsImmediateChild<RectTransform>().ToList();
    }

    public override void OnUpdateLayout()
    {
        base.OnUpdateLayout();

        if (childRectTransforms.IsEmpty())
            return;

        int columnCount = fixedColumnCount;
        int rowCount = Mathf.CeilToInt((float)childRectTransforms.Count / columnCount);

        float cellWidth = (rectTransform.rect.width - (columnCount - 1) * spacing.x) / columnCount;
        float cellHeight = (rectTransform.rect.height - (rowCount - 1) * spacing.y) / rowCount;

        for (int i = 0; i < childRectTransforms.Count; i++)
        {
            int row = i / columnCount;
            int column = i % columnCount;

            RectTransform child = childRectTransforms[i];

            // Set the anchor to top-left
            child.anchorMin = new Vector2(0, 1);
            child.anchorMax = new Vector2(0, 1);
            child.pivot = new Vector2(0, 1);

            float xPos = column * (cellWidth + spacing.x);
            float yPos = -row * (cellHeight + spacing.y);

            child.anchoredPosition = new Vector2(xPos, yPos);
            child.sizeDelta = new Vector2(cellWidth, cellHeight);
        }
    }
}
