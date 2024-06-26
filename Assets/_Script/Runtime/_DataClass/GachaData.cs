using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GachaTable
{
    public List<GachaPool> gachaPoolClient;

    public static GachaTable FromJson(string jsonText)
    {
        return SaveSystem.LoadJson_String<GachaTable>(jsonText);
    }
}

[Serializable]
public class GachaPool
{
    public string gachaPoolId;
    public int gachaIndex;
    public long openTime;
    public long endTime;
    public string gachaPoolName;
    public string gachaPoolSummary;
    public string gachaPoolDetail;
    public int guarantee5Avail;
    public int guarantee5Count;
    public string LMTGSID;
    public string CDPrimColor;
    public string CDSecColor;
    public string gachaRuleType;
    public DynMeta dynMeta;
    public string linkageRuleId;
    public object linkageParam;
}

[Serializable]
public class DynMeta
{
    public string main6RarityCharId;
    public List<string> rare5CharList;
    public int scrollIndex;
    public string sub6RarityCharId;
    public string chooseRuleConst;
    public string homeDescConst;
    public Dictionary<string, List<string>> rarityPickCharDict;
    public string star5ChooseRuleConst;
    public string star6ChooseRuleConst;
}