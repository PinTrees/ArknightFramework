using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public static class RectTransformEx
{
    public static void SetAncherLeftTop(this RectTransform rectTransform)
    {
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
    }

    public static Rect CalculateRectTransformsBounds(List<RectTransform> rectTransforms)
    {
        if (rectTransforms.Count == 0)
            return new Rect(0, 0, 0, 0);

        float minX = float.MaxValue, minY = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue;

        foreach (RectTransform child in rectTransforms)
        {
            Vector2 childPosition = child.position;
            Vector2 size = child.rect.size;
            Vector2 min = childPosition - (size * 0.5f);
            Vector2 max = childPosition + (size * 0.5f);

            minX = Mathf.Min(minX, min.x);
            minY = Mathf.Min(minY, min.y);
            maxX = Mathf.Max(maxX, max.x);
            maxY = Mathf.Max(maxY, max.y);
        }

        // 중심 좌표와 크기를 계산
        Vector2 center = new Vector2((minX + maxX) / 2, (minY + maxY) / 2);
        Vector2 sizeDelta = new Vector2(maxX - minX, maxY - minY);

        // Rect 반환
        return new Rect(center.x - sizeDelta.x * 0.5f, center.y - sizeDelta.y * 0.5f, sizeDelta.x, sizeDelta.y);
    }
  
    public static float SumRectTransformsWidth(List<RectTransform> rectTransforms)
    {
        float sum = 0;
        rectTransforms.ForEach(e => sum += e.rect.width);
        return sum;
    }
    public static float SumRectTransformsHeight(List<RectTransform> rectTransforms)
    {
        float sum = 0;
        rectTransforms.ForEach(e => sum += e.rect.height);
        return sum;
    }
}
