using UnityEngine;

public class InputManager : Singleton_Mono<InputManager>
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (UIManager.Instance.GetView<UI_Cheet_Main>().IsShow())
                UIManager.Instance.GetView<UI_Cheet_Main>().Close();
            else UIManager.Instance.GetView<UI_Cheet_Main>().Show();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (UIManager.Instance.GetView<UI_CharacterList_View>().IsShow())
                UIManager.Instance.GetView<UI_CharacterList_View>().Close();
            else UIManager.Instance.GetView<UI_CharacterList_View>().Show();
        }
    }
}
