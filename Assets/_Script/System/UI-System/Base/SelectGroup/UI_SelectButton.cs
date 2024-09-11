using UnityEngine;
using UnityEngine.UI;

public class UI_SelectButton : UIObjectBase
{
    [Header("UI Components Setting")]
    public Button button;
    public GameObject selectedObject;
    public GameObject defaultObject;

    [Header("Runtime Value")]
    public UI_SelectButtonGroup group;


    protected override void OnInit()
    {
        base.OnInit();

        group = GetComponentInParent<UI_SelectButtonGroup>();   

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            group.SetSelect(this);
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

    public virtual void OnSelect(bool active)
    {

    }
}
