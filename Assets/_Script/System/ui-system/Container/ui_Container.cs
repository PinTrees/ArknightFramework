using UnityEngine;

[ExecuteInEditMode]
public class ui_Container : UILayoutBase
{
    public UIObjectBase child;

    public FlexFit rowAxisFit = FlexFit.Fixed;
    public FlexFit columnAxisFit = FlexFit.Fixed;

    public Alignment alignment = Alignment.Center;


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

        child = target.GetComponentImmediateFirstChild<UIObjectBase>();
    }

    public override void OnUpdateLayout()
    {
        base.OnUpdateLayout();

        // Step 1: Calulate Row Axis RectTransform
        switch (rowAxisFit)
        {
            case FlexFit.Tight:
                FlexFitUtill.ApplyFlexFit(rowAxisFit, this, child: child, axis: Axis.Row);
                EdgeInsets.ApplyPadding(padding, this, child, Axis.Row);
                break;
            case FlexFit.Loose:
                FlexFitUtill.ApplyFlexFit_Loose(this, parent, Axis.Row);
                break;
            default:
                break;
        }

        // Step 2: Calulate Column Axis RectTransform
        switch(columnAxisFit)
        {
            case FlexFit.Tight:
                FlexFitUtill.ApplyFlexFit(columnAxisFit, this, child: child, axis: Axis.Column);
                EdgeInsets.ApplyPadding(padding, this, child, Axis.Column);
                break;
            case FlexFit.Loose:
                FlexFitUtill.ApplyFlexFit(columnAxisFit, this, parent: parent, axis: Axis.Column);
                break;
        }

        if (child == null)
            return;

        AlignmentUtill.ApplyAlignment(alignment, this, child);
    }
}
