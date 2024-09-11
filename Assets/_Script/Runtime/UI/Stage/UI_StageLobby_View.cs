using UnityEngine;

public class UI_StageLobby_View : UIViewBase
{
    protected override void OnInit()
    {
        base.OnInit();
    }

    public override void Show()
    {
        base.Show();
        base.ShowAnimation();

        UISystemManager.Instance.GetView<UI_TopTab>().AddUndo(() =>
        {
            Close();
        }).Show();
    }

    public override void Close()
    {
        base.CloseAnimation(() =>
        {
            base.Close();
        });
    }
}
