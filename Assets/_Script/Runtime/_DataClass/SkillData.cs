using System.Collections.Generic;
using System;

[Serializable]
public class SkillData
{
    public string skillId;
    public string iconId;
    public bool hidden;
    public List<SkillLevel> levels;
}

[Serializable]
public class SkillLevel
{
    public string name;
    public string rangeId;
    public string description;
    public string skillType;
    public string durationType;
    public SPData spData;
    public string prefabId;
    public float duration;
    public List<BlackboardEntry> blackboard;
}

[Serializable]
public class SPData
{
    public string spType;
    public object levelUpCost;
    public int maxChargeTime;
    public int spCost;
    public int initSp;
    public float increment;
}

[Serializable]
public class BlackboardEntry
{
    public string key;
    public float value;
    public string valueStr;
}