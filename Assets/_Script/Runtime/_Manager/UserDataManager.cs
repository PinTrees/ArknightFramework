using UnityEngine;

public class UserDataManager : Singleton_Mono<UserDataManager>
{
    public const string USERDATA_SAVE_PATH = "Database/user_table.json";

    public TextAsset userdata_table;

    public UserData userData { get; private set; }


    public override void Init()
    {
        base.Init();

        userData = UserData.FromJson(userdata_table.text);
    }

    public void GetCharacter(Character character)
    {
        userData.characters.Add(new UserCharacterData()
        {
            char_uid = character.id,
        });
        SaveData();
    }

    public void SaveData()
    {
        SaveSystem.SaveJson_AssetPath(userData, USERDATA_SAVE_PATH);
    }

#if UNITY_EDITOR
    [Button("Setup User Data Json")]
    public void _Editor_SetupUserData()
    {
        //userDataJson;
        UserData userData = new();
        userData.name = "Test user data";

        SaveSystem.SaveJson_AssetPath(userData, USERDATA_SAVE_PATH);
    }
    [Button("Refresh User Data")]
    public void _Editor_RefreshUserData()
    {
        //userDataJson;
        UserData userData = UserData.FromJson(userdata_table.text);
        SaveSystem.SaveJson_AssetPath(userData, USERDATA_SAVE_PATH);
    }
#endif
}
