using System.Collections.Generic;
using UnityEngine;

public class EdgeInsets
{
    public static EdgeInsetsData All(float amount)
    {
        return new EdgeInsetsData()
        {
            Left = amount,
            Top = amount,
            Right = amount,
            Bottom = amount,
        };
    }

    public static void ApplyPadding(EdgeInsetsData padding, RectTransform parentRectTransform, List<RectTransform> childRectTransforms)
    {
        if (padding.Left == 0 && padding.Top == 0 && padding.Right == 0 && padding.Bottom == 0)
            return;

        if (parentRectTransform == null)
            return;

        parentRectTransform.sizeDelta = new Vector2
        (
            x: parentRectTransform.sizeDelta.x + padding.Left + padding.Right,
            y: parentRectTransform.sizeDelta.y + padding.Top + padding.Bottom
        );

        // �ڽ� RectTransform���� ��ġ ����
        foreach (var childRectTransform in childRectTransforms)
        {
            // ���� �ڽ��� anchoredPosition�� ������
            Vector2 childAnchoredPosition = childRectTransform.anchoredPosition;

            // �ڽ��� anchoredPosition�� �е� ���� ���� �̵�
            childAnchoredPosition.x += padding.Left;
            childAnchoredPosition.y -= padding.Top;

            // ����� �� ����
            childRectTransform.anchoredPosition = childAnchoredPosition;
        }
    }

    public static void ApplyPadding(EdgeInsetsData padding, UIObjectBase target, UIObjectBase child, Axis axis = Axis.All)
    {
        if (padding.Left == 0 && padding.Top == 0 && padding.Right == 0 && padding.Bottom == 0)
            return;

        if (target == null)
            return;

        if (child == null)
            return;

        target.rectTransform.sizeDelta = new Vector2()
        {
            x = target.rectTransform.sizeDelta.x + (axis == Axis.Column ? 0 : padding.Left + padding.Right),
            y = target.rectTransform.sizeDelta.y + (axis == Axis.Row ? 0 : padding.Top + padding.Bottom),
        };

        if (child == null)
            return;

        child.rectTransform.anchoredPosition = new Vector2()
        {
            x = child.rectTransform.anchoredPosition.x + (axis == Axis.Column ? 0 : padding.Left),
            y = child.rectTransform.anchoredPosition.y - (axis == Axis.Row ? 0 : padding.Top),
        };
    }
}
