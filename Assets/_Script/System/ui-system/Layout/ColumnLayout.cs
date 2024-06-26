using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class ColumnLayout : UILayoutBase
{
    public List<RectTransform> childRectTransforms = new();

    public float spacing = 0;
    public MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start;
    public CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Center;

    public FlexFit mainAxisFit = FlexFit.Tight;
    public FlexFit crossAxisFit = FlexFit.Tight;


    private void Reset()
    {
        Init();
        SetLayout();
    }

    protected override void OnInit()
    {
        base.OnInit();
    }

    public override void SetLayout()
    {
        base.SetLayout();

        childRectTransforms = target.GetComponentsImmediateChild<RectTransform>().ToList();
    }

    public override void OnUpdateLayout()
    {
        if (childRectTransforms == null)
            return;

        float width = 0;
        float height = 0;
        float childHeightSum = RectTransformEx.SumRectTransformsHeight(childRectTransforms);
        float childSpacingSum = spacing * childRectTransforms.Count - 1;

        // Step 1: Calculate the total width and maximum height of children
        foreach (var child in childRectTransforms)
        {
            height += child.sizeDelta.y + spacing;
            if (child.sizeDelta.x > width)
            {
                width = child.sizeDelta.x;
            }
        }
        height -= spacing;

        switch(mainAxisFit)
        {
            case FlexFit.Tight:
                break;
            case FlexFit.Loose:
                if(parent != null)
                {
                    height = parent.rectTransform.rect.height;
                    if(parent is UILayoutBase layout)
                    {
                        height -= (layout.padding.Top + layout.padding.Bottom);
                    }
                }
                break;
            case FlexFit.Fixed:
                height = rectTransform.rect.height - padding.Top - padding.Bottom;
                break;
            default:
                break;
        }

        switch(crossAxisFit)
        {
            case FlexFit.Tight:
                break;
            case FlexFit.Loose:
                if (parent != null)
                {
                    width = parent.rectTransform.rect.width;
                    if (parent is UILayoutBase layout)
                    {
                        width -= (layout.padding.Left + layout.padding.Right);
                    }
                }
                break;
            case FlexFit.Fixed:
                width = rectTransform.rect.width - padding.Left - padding.Right;
                break;
            default:
                break;
        }

        // Step 2: Update the parent's RectTransform size
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.sizeDelta = new Vector2(width, height);

        // Step 3: Calculate the starting position based on the alignment
        float startY = 0;
        switch (mainAxisAlignment)
        {
            case MainAxisAlignment.Center:
                startY = (rectTransform.rect.height - childHeightSum - childSpacingSum + spacing) / 2;
                break;
            case MainAxisAlignment.End:
                startY = rectTransform.rect.height - childHeightSum - childSpacingSum + spacing;
                break;
            case MainAxisAlignment.Start:
            default:
                startY = 0;
                break;
        }

        // Step 4: Reposition the children based on the new parent's size
        float currentY = startY;
        foreach (var child in childRectTransforms)
        {
            // Set the anchor to top-left
            child.anchorMin = new Vector2(0, 1);
            child.anchorMax = new Vector2(0, 1);
            child.pivot = new Vector2(0, 1); // Optional: Ensure the pivot is also top-left

            // Calculate the y position based on the crossAxisAlignment
            float xPosition = 0;
            switch (crossAxisAlignment)
            {
                case CrossAxisAlignment.Center:
                    xPosition = ((child.rect.width - width) / 2);
                    break;
                case CrossAxisAlignment.End:
                    xPosition = child.rect.width - width;
                    break;
                case CrossAxisAlignment.Start:
                default:
                    xPosition = 0;
                    break;
            }

            // Set the anchored position
            child.anchoredPosition = new Vector2(-xPosition, -currentY);
            currentY += child.sizeDelta.y + spacing;
        }

        // Step 5: Set Padding 
        EdgeInsets.ApplyPadding(padding, rectTransform, childRectTransforms);
    }

    private void OnDrawGizmos()
    {
        if (rectTransform == null)
            return;

        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        Gizmos.color = Color.red;

        for (int i = 0; i < 4; i++)
        {
            Vector3 from = corners[i];
            Vector3 to = corners[(i + 1) % 4];
            Gizmos.DrawLine(from, to);
        }
    }
}
