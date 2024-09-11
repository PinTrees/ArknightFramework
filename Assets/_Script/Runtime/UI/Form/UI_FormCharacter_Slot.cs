using UnityEngine;
using UnityEngine.UI;

public class UI_FormCharacter_Slot : UIObjectBase
{
    [Header("UI Components Setting")]
    public Button button;
    public Button characterIconButton;

    protected override void OnInit()
    {
        base.OnInit();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            UISystemManager.Instance.GetView<UI_FormCharacterSelect_View>().Show();
        });
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Close()
    {
        base.Close();
    }
}
