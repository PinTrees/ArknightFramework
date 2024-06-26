using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu_View : UIViewBase
{
    public Button chatacterListButton;

    public Text currentDateText;

    [Header("user info component")]
    public Text userLevelText;
    public Text userNameText;
    public Text userIdText;
    [Space]
    public Text userGoldText;
    public Text userRedgemText;
    public Text userPuregemText;


    protected override void OnInit()
    {
        base.OnInit();

        chatacterListButton.onClick.RemoveAllListeners();
        chatacterListButton.onClick.AddListener(() =>
        {
            UIManager.Instance.GetView<UI_CharacterList_View>().Show();
        });
    }

    public override void Show()
    {
        base.Show();

        DateTime now = DateTime.Now;
        currentDateText.text = $"{now.ToString("yyyy/MM/dd HH:mm").Replace("-", "/")}\\";

        var userData = UserDataManager.Instance.userData;

        userLevelText.text = userData.level.ToString();
        userNameText.text = userData.name;
    }

    public override void Close()
    {
        base.Close();
    }
}
