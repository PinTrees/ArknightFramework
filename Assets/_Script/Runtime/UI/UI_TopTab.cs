using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TopTab : UIViewBase
{
    public Button backButton;
    public Button homeButton;

    public Stack<Action> backActionStatck = new();


    protected override void OnInit()
    {
        base.OnInit();

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            if (backActionStatck.Count > 0)
            {
                var action = backActionStatck.Pop();
                action();
            }

            if (backActionStatck.Count <= 0)
                Close();
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

    public UI_TopTab AddUndo(Action action)
    {
        backActionStatck.Push(action);
        return this;
    }
}
