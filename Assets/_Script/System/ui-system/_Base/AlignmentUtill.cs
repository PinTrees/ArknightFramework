using UnityEngine;

public class AlignmentUtill
{
    public static void ApplyAlignment(Alignment alignment, UILayoutBase target, UIObjectBase child)
    {
        if (target == null || child == null) return;

        EdgeInsetsData padding = target.padding;
        EdgeInsetsData margin = target.margin;

        Vector2 parentSize = target.rectTransform.rect.size;
        Vector2 childSize = child.rectTransform.rect.size;
        Vector2 newPosition = Vector2.zero;

        // Apply padding
        float paddedWidth = parentSize.x - padding.Left - padding.Right;
        float paddedHeight = parentSize.y - padding.Top - padding.Bottom;

        switch (alignment)
        {
            case Alignment.TopLeft:
                newPosition = new Vector2(padding.Left + margin.Left, -padding.Top - margin.Top);
                break;
            case Alignment.TopCenter:
                newPosition = new Vector2(padding.Left + (paddedWidth - childSize.x) / 2, -padding.Top - margin.Top);
                break;
            case Alignment.TopRight:
                newPosition = new Vector2(padding.Left + paddedWidth - childSize.x - margin.Right, -padding.Top - margin.Top);
                break;
            case Alignment.CenterLeft:
                newPosition = new Vector2(padding.Left + margin.Left, -padding.Top - (paddedHeight - childSize.y) / 2);
                break;
            case Alignment.Center:
                newPosition = new Vector2(padding.Left + (paddedWidth - childSize.x) / 2, -padding.Top - (paddedHeight - childSize.y) / 2);
                break;
            case Alignment.CenterRight:
                newPosition = new Vector2(padding.Left + paddedWidth - childSize.x - margin.Right, -padding.Top - (paddedHeight - childSize.y) / 2);
                break;
            case Alignment.BottomLeft:
                newPosition = new Vector2(padding.Left + margin.Left, -padding.Top - paddedHeight + childSize.y + margin.Bottom);
                break;
            case Alignment.BottomCenter:
                newPosition = new Vector2(padding.Left + (paddedWidth - childSize.x) / 2, -padding.Top - paddedHeight + childSize.y + margin.Bottom);
                break;
            case Alignment.BottomRight:
                newPosition = new Vector2(padding.Left + paddedWidth - childSize.x - margin.Right, -padding.Top - paddedHeight + childSize.y + margin.Bottom);
                break;
        }

        // Apply the calculated position
        child.rectTransform.anchoredPosition = newPosition;
    }
}
