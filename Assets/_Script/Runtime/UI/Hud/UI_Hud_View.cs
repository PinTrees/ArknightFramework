using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud_View : UIViewBase
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
            if(backActionStatck.Count > 0)
            {
                var action = backActionStatck.Pop();
                action();
            }

            if (backActionStatck.Count <= 0)
                Close();
        });

        Close();
    }

    public void AddBackAction(Action action)
    {
        backActionStatck.Push(action);
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
