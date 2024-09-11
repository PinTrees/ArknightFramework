using UnityEngine;

public class UIManager_Lobby : Singleton<UIManager_Lobby>
{
    public override void Init()
    {
        UISystemManager.Instance.GetView<UI_TopTab>().Close();

        UISystemManager.Instance.GetView<UI_FormSetting_View>().Close();
        UISystemManager.Instance.GetView<UI_CharacterList_View>().Close();
        UISystemManager.Instance.GetView<UI_StageLobby_View>().Close();
        UISystemManager.Instance.GetView<UI_FormCharacterSelect_View>().Close();
    }
}
