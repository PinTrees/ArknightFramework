using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu_View : UIViewBase
{
    [Header("Menu Button Setting")]
    public Button chatacterListButton;
    public Button formSettingButton;
    public Button stageLobbyButton;

    [Space]
    public Text currentDateText;

    [Header("User Info Component Setting")]
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
            UISystemManager.Instance.GetView<UI_CharacterList_View>().Show();
        });

        formSettingButton.onClick.RemoveAllListeners();
        formSettingButton.onClick.AddListener(() =>
        {
            UISystemManager.Instance.GetView<UI_FormSetting_View>().Show();
        });

        stageLobbyButton.onClick.RemoveAllListeners();
        stageLobbyButton.onClick.AddListener(() =>
        {
            UISystemManager.Instance.GetView<UI_StageLobby_View>().Show();
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
