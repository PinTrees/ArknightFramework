using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        UserDataManager.Instance.Init();
        DatabaseManager.Instance.Init();
        ResourceManager.Instance.Init();

        UISystemManager.Instance.GetView<UI_MainMenu_View>().Show();
    }

    void Update()
    {
        
    }
}
