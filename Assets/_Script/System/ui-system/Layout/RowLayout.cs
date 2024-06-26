using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[ExecuteInEditMode]
public class RowLayout : UILayoutBase
{
    public List<RectTransform> rectTransforms = new();
    public float spacing;

    public FlexFit mainAxisFit = FlexFit.Tight;
    public FlexFit crossAxisFit = FlexFit.Tight;
    
    public MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start;
    public CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Center;


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

        rectTransforms = target.GetComponentsImmediateChild<RectTransform>().ToList();
    }

    public override void OnUpdateLayout()
    {
        base.OnUpdateLayout();

        if (rectTransforms == null)
            return;

        float width = 0;
        float height = 0;
        float childWidthSum = RectTransformEx.SumRectTransformsWidth(rectTransforms);
        float childSpacingSum = spacing * rectTransforms.Count - 1;

        // Step 1: Calculate the total width and maximum height of children
        foreach (var child in rectTransforms)
        {
            width += child.sizeDelta.x + spacing;
            if (child.sizeDelta.y > height)
            {
                height = child.sizeDelta.y;
            }
        }
        width -= spacing;

        // Step 1.5: Calculate Fit
        switch (mainAxisFit)
        {
            case FlexFit.Loose:
                if(parent != null)
                {
                    float startWidth = 0;
                    width = parent.rectTransform.rect.width;
                    if (parent is UILayoutBase layout)
                    {
                        width -= (layout.padding.Left + layout.padding.Right);
                        startWidth = layout.padding.Left;
                    }
                    rectTransform.anchoredPosition = new Vector2(startWidth, rectTransform.anchoredPosition.y);
                }
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
        float startX = 0;
        switch (mainAxisAlignment)
        {
            case MainAxisAlignment.Center:
                startX = (rectTransform.rect.width - childWidthSum - childSpacingSum + spacing) / 2;
                break;
            case MainAxisAlignment.End:
                startX = rectTransform.rect.width - childWidthSum - childSpacingSum + spacing;
                break;
            case MainAxisAlignment.Start:
            default:
                startX = 0;
                break;
        }

        // Step 4: Reposition the children based on the new parent's size
        float currentX = startX;
        foreach (var child in rectTransforms)
        {
            // Set the anchor to top-left
            child.anchorMin = new Vector2(0, 1);
            child.anchorMax = new Vector2(0, 1);
            child.pivot = new Vector2(0, 1); // Optional: Ensure the pivot is also top-left

            // Calculate the y position based on the crossAxisAlignment
            float yPosition = 0;
            switch (crossAxisAlignment)
            {
                case CrossAxisAlignment.Center:
                    yPosition = ((child.rect.height - height) / 2);
                    break;
                case CrossAxisAlignment.End:
                    yPosition = child.rect.height - height;
                    break;
                case CrossAxisAlignment.Start:
                default:
                    yPosition = 0;
                    break;
            }

            // Set the anchored position
            child.anchoredPosition = new Vector2(currentX, yPosition);
            currentX += child.sizeDelta.x + spacing;
        }
    }

    private void OnDrawGizmos()
    {
        if (rectTransform == null)
            return;

        // 현재 UI 오브젝트의 월드 좌표로 변환된 네 구석 좌표를 가져옴
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // Gizmos 색깔 설정
        Gizmos.color = Color.blue;

        // 네 구석을 연결하여 사각형 그리기
        for (int i = 0; i < 4; i++)
        {
            Vector3 from = corners[i];
            Vector3 to = corners[(i + 1) % 4];
            Gizmos.DrawLine(from, to);
        }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            //SetLayout();
        }
    }
}
