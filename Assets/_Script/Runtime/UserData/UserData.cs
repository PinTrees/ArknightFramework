using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string name;
    public uint level;
    public int id;
    public string main_character_id;

    // cash
    public int gold;
    public int redgem;
    public int puregem;

    public List<UserCharacterData> characters = new();
    public List<UserItemData> items = new();

    public static UserData FromJson(string json)
    {
        UserData userData = SaveSystem.LoadJson_String<UserData>(json);

        if(userData.main_character_id == null)
            userData.main_character_id = userData.characters.First().char_uid;

        return userData;
    }
}

[System.Serializable]
public class UserCharacterData
{
    public string char_uid;

    public int level;
    public int exp;
    public int potential;
}

[System.Serializable]
public class UserItemData
{
    public string item_uid;
    public string limit_duration;
}

