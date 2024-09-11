using UnityEngine;

public class InputManager : Singleton_Mono<InputManager>
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (UISystemManager.Instance.GetView<UI_Cheet_Main>().IsShow())
                UISystemManager.Instance.GetView<UI_Cheet_Main>().Close();
            else UISystemManager.Instance.GetView<UI_Cheet_Main>().Show();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (UISystemManager.Instance.GetView<UI_CharacterList_View>().IsShow())
                UISystemManager.Instance.GetView<UI_CharacterList_View>().Close();
            else UISystemManager.Instance.GetView<UI_CharacterList_View>().Show();
        }
    }
}
