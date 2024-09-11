using DG.Tweening;
using UnityEngine;

public class UI_FormSelectButton : UI_SelectButton
{
    [Header("Data Setting")]
    public Color selectButtonColor;
    public Color deSelectButtonColor;
    
    protected override void OnInit()
    {
        base.OnInit();
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Close()
    {
        base.Close();
    }

    public override void OnSelect(bool active)
    {
        base.OnSelect(active);

        selectedObject.SetActive(false);
        defaultObject.SetActive(false);

        button.targetGraphic.DOKill();
        if (active) button.targetGraphic.DOColor(selectButtonColor, 0.35f);
        else button.targetGraphic.DOColor(deSelectButtonColor, 0.25f);

        if (active) selectedObject.SetActive(true);
        else defaultObject.SetActive(true);
    }
}
