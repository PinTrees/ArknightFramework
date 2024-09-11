using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UI_FormSetting_View : UIViewBase
{
    [Header("UI Components Setting")]
    public Button formGroupRightArrowButton;
    public Button formGropLeftArrowButton;
    public UI_SelectButtonGroup formSelectGroup;

    [Header("Runtime Value")]
    public List<UI_FormCharacter_Slot> slots = new();

    protected override void OnInit()
    {
        base.OnInit();

        slots = baseObject.GetComponentsInChildren<UI_FormCharacter_Slot>().ToList();

        formGroupRightArrowButton.onClick.RemoveAllListeners();
        formGropLeftArrowButton.onClick.RemoveAllListeners();

        formGroupRightArrowButton.onClick.AddListener(() =>
        {
            formSelectGroup.SetSelectNext();
        });
        formGropLeftArrowButton.onClick.AddListener(() =>
        {
            formSelectGroup.SetSelectPre();
        });
    }

    public override void Show()
    {
        base.Show();
        base.ShowAnimation();

        formSelectGroup.Show();

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
