using UnityEngine;


public class FlexFitUtill
{
    public static void ApplyFlexFit_Loose(UILayoutBase target, UIObjectBase parent, Axis axis = Axis.All)
    {
        if (target == null)
            return;
        if (parent == null)
            return;

        // Step 1: Get the padding and margin values from the parent and target objects
        float parentWidth = parent.rectTransform.rect.width;
        float targetHeight = target.rectTransform.rect.height;

        EdgeInsetsData padding = (parent is UILayoutBase layout) ? layout.padding : EdgeInsets.All(0);
        //EdgeInsetsData margin = (target is UILayoutBase target_layout) ? target_layout.margin : EdgeInsets.All(0);

        float newWidth = parentWidth - padding.Left - padding.Right; //- margin.Left - margin.Right;
        float newHeight = parentWidth - padding.Left - padding.Right; // - margin.Left - margin.Right;

        // Step 3: set Target RectTransform
        target.rectTransform.sizeDelta = new Vector2()
        {
            x = axis == Axis.Row ? newWidth : 0,
            y = axis == Axis.Column ? 0 : targetHeight,
        };
    }

    public static void ApplyFlexFit(FlexFit mode, UILayoutBase target,
        UIObjectBase parent=null,
        UIObjectBase child = null,
        Axis axis=Axis.All)
    {
        if (target == null)
            return;
        if (mode == FlexFit.Tight && child == null)
            return;
        if (mode == FlexFit.Loose && parent == null)
            return;

        if (axis == Axis.All)
        {
            if(mode == FlexFit.Tight)
            {
                target.rectTransform.sizeDelta = new Vector2()
                {

                };
            }
        }
        else if(axis == Axis.Row)
        {
            if (mode == FlexFit.Tight)
            {
                target.rectTransform.sizeDelta = new Vector2()
                {
                    x = child.rectTransform.rect.width,
                    y = target.rectTransform.rect.height,
                };
            }
        }
        else if(axis == Axis.Column)
        {
            if(mode == FlexFit.Tight)
            {
                target.rectTransform.sizeDelta = new Vector2()
                {
                    x = target.rectTransform.rect.width,
                    y = child.rectTransform.rect.height,
                };
            }
            else if(mode == FlexFit.Loose)
            {
                target.rectTransform.sizeDelta = new Vector2()
                { 
                    x = target.rectTransform.rect.width,
                    y = parent.rectTransform.rect.height - (parent is UILayoutBase l ? (l.padding.Top + l.padding.Bottom) : 0),
                };
            }
        }
    }
}
