using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextButtonUI : UIObjectBase
{
    // component
    public TextUI text;
    public Image image;
    public Button button;

    // style data
    public EdgeInsetsData padding = EdgeInsets.All(8);
    public FlexFit rowFit = FlexFit.Tight;
    public FlexFit columnFit = FlexFit.Tight;


    private void Reset()
    {
        Init();
    }

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    protected override void OnInit()
    {
        base.OnInit();

        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        image = target.AddComponent<Image>();
        button = target.AddComponent<Button>();
        text = target.CreateChildWithComponent<TextUI>("Text");

        button.targetGraphic = image;
        text.textComponent.raycastTarget = false;
        text.text = "Text";
        text.Refresh();
    }

    public override void Show()
    {
        base.Show();

        text.Refresh();
    }

    public void SetClickAction(Action action)
    {
        if (button == null)
            return;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { action(); });
    }

    protected override void LateUpdate()
    {
        if (target == null)
            return;

        rectTransform.sizeDelta = text.rectTransform.sizeDelta;
        text.rectTransform.anchoredPosition = Vector2.zero;

        switch(rowFit)
        {
            case FlexFit.Tight:
                EdgeInsets.ApplyPadding(padding, this, text, Axis.Row);
                break;
            case FlexFit.Loose:
                if (parent == null) 
                    break;
                rectTransform.sizeDelta = parent.rectTransform.sizeDelta;
                break;
        }

        switch(columnFit)
        {
            case FlexFit.Tight:
                EdgeInsets.ApplyPadding(padding, this, text, Axis.Column);
                break;
            case FlexFit.Loose:
                break;
        }
    }
}
