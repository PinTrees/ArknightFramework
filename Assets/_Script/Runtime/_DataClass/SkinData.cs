using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class SkinData
{
    public string skinId;
    public string charId;
    public object tokenSkinMap;     // Replace with appropriate type if known
    public string illustId;
    public object dynIllustId;      // Replace with appropriate type if known
    public string avatarId;
    public string portraitId;
    public object dynPortraitId;    // Replace with appropriate type if known
    public object dynEntranceId;    // Replace with appropriate type if known
    public object buildingId;       // Replace with appropriate type if known
    public BattleSkin battleSkin;
    public bool isBuySkin;
    public object tmplId;           // Replace with appropriate type if known
    public object voiceId;          // Replace with appropriate type if known
    public string voiceType;
    public DisplaySkin displaySkin;
}

[Serializable]
public class BattleSkin
{
    public bool overwritePrefab;
    public string skinOrPrefabId;
}

[Serializable]
public class DisplaySkin
{
    public string skinName;
    public List<string> colorList;
    public List<string> titleList;
    public string modelName;
    public List<string> drawerList;
    public List<string> designerList;
    public string skinGroupId;
    public string skinGroupName;
    public int skinGroupSortIndex;
    public string content;
    public object dialog;           // Replace with appropriate type if known
    public object usage;            // Replace with appropriate type if known
    public object description;      // Replace with appropriate type if known
    public object obtainApproach;   // Replace with appropriate type if known
    public int sortId;
    public object displayTagId;     // Replace with appropriate type if known
    public int getTime;
    public int onYear;
    public int onPeriod;
}

[Serializable]
public class SkinTable
{
    public Dictionary<string, SkinData> charSkins;

    public static SkinTable FromJson(string jsonText)
    {
        return SaveSystem.LoadJson_String<SkinTable>(jsonText);
    }
}

[Serializable]
public class SkinSearchTable
{
    public Dictionary<string, List<SkinData>> charaterSkins = new();

    public static SkinSearchTable FromData(SkinTable skinTable)
    {
        SkinSearchTable s = new SkinSearchTable();

        foreach (var item in skinTable.charSkins)
        {
            if (!s.charaterSkins.ContainsKey(item.Value.charId))
                s.charaterSkins[item.Value.charId] = new();

            s.charaterSkins[item.Value.charId].Add(item.Value);
        }

        return s;
    }

    public SkinData Search_Elite0(string charId)
    {
        if (!charaterSkins.ContainsKey(charId))
            return null;

        return charaterSkins[charId].FirstOrDefault(e => e.displaySkin.sortId == -3);
    }

    public SkinData Search_Elite1(string charId)
    {
        if (!charaterSkins.ContainsKey(charId))
            return null;

        return charaterSkins[charId].FirstOrDefault(e => e.displaySkin.sortId == -2);
    }

    public SkinData Search_Elite2(string charId)
    {
        if (!charaterSkins.ContainsKey(charId))
            return null;

        return charaterSkins[charId].FirstOrDefault(e => e.displaySkin.sortId == -1);
    }
}