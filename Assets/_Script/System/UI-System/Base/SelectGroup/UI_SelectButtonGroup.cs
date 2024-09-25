using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_SelectButtonGroup : UIObjectBase
{
    //[Header("Transform Setting")]

    [Header("Runtime Value")]
    [SerializeField] private List<UI_SelectButton> selectButtons = new();
    [SerializeField] public int currentSelectIndex;

    protected override void OnInit()
    {
        base.OnInit();

        currentSelectIndex = 0;
        selectButtons = baseObject.GetComponentsInChildren<UI_SelectButton>().ToList();
        selectButtons.ForEach(e =>
        {
            e.OnSelect(false);
        });
    }

    public override void Show()
    {
        base.Show();

        currentSelectIndex = 0;

        selectButtons.ForEach(e => e.OnSelect(false)); 
        selectButtons.First().OnSelect(true);
    }

    public override void Close()
    {
        base.Close();
    }

    public void SetSelect(UI_SelectButton button)
    {
        selectButtons.ForEach(e => e.OnSelect(false));
        button.OnSelect(true);
    }

    public void SetSelect(int index)
    {
        currentSelectIndex = index;
        selectButtons.ForEach(e => e.OnSelect(false));
        selectButtons[currentSelectIndex].OnSelect(true);
    }

    public void SetSelectNext()
    {
        currentSelectIndex++;
        if (currentSelectIndex >= selectButtons.Count)
            currentSelectIndex = 0;

        SetSelect(currentSelectIndex);
    }
    public void SetSelectPre()
    {
        currentSelectIndex--;
        if (currentSelectIndex < 0)
            currentSelectIndex = selectButtons.Count - 1;

        SetSelect(currentSelectIndex);
    }
}
