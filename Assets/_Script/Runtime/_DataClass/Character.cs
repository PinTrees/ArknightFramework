using System;
using System.Collections.Generic;
using System.Diagnostics;

[Serializable]
public class Character
{
    public string id;
    public string name;
    public string description;
    public bool canUseGeneralPotentialItem;
    public bool canUseActivityPotentialItem;
    public string potentialItemId;
    public string activityPotentialItemId;
    public string classicPotentialItemId;
    public string nationId;
    public string groupId;
    public string teamId;
    public string displayNumber;
    public string appellation;
    public string position;
    public List<string> tagList;
    public string itemUsage;
    public string itemDesc;
    public string itemObtainApproach;
    public bool isNotObtainable;
    public bool isSpChar;
    public int maxPotentialLevel;
    public string rarity;
    public string profession;
    public string subProfessionId;
    public List<Phase> phases;
    public List<Skill> skills;
    public List<Talent> talents;
    public List<PotentialRank> potentialRanks;
    public List<FavorKeyFrame> favorKeyFrames;
    public List<SkillLevelUp> allSkillLvlup;
}

[Serializable]
public class Phase
{
    public string characterPrefabKey;
    public string rangeId;
    public int maxLevel;
    public List<AttributesKeyFrame> attributesKeyFrames;
    public object evolveCost;
}

[Serializable]
public class AttributesKeyFrame
{
    public int level;
    public AttributesData data;
}

[Serializable]
public class AttributesData
{
    public int maxHp;
    public int atk;
    public int def;
    public float magicResistance;
    public int cost;
    public int blockCnt;
    public float moveSpeed;
    public float attackSpeed;
    public float baseAttackTime;
    public int respawnTime;
    public float hpRecoveryPerSec;
    public float spRecoveryPerSec;
    public int maxDeployCount;
    public int maxDeckStackCnt;
    public int tauntLevel;
    public int massLevel;
    public int baseForceLevel;
    public bool stunImmune;
    public bool silenceImmune;
    public bool sleepImmune;
    public bool frozenImmune;
    public bool levitateImmune;
}

[Serializable]
public class Skill
{
    // Define skill properties here
}

[Serializable]
public class Talent
{
    public List<Candidate> candidates;
}

[Serializable]
public class Candidate
{
    public UnlockCondition unlockCondition;
    public int requiredPotentialRank;
    public string prefabKey;
    public string name;
    public string description;
    public string rangeId;
    public List<Blackboard> blackboard;
    public string tokenKey;
}

[Serializable]
public class UnlockCondition
{
    public string phase;
    public int level;
}

[Serializable]
public class Blackboard
{
    public string key;
    public float value;
    public string valueStr;
}

[Serializable]
public class PotentialRank
{
    public string type;
    public string description;
    public object buff;
    public object equivalentCost;
}

[Serializable]
public class FavorKeyFrame
{
    public int level;
    public AttributesData data;
}

[Serializable]
public class SkillLevelUp
{
    // Define skill level up properties here
}

[Serializable]
public class CharacterTable
{
    public Dictionary<string, Character> characters;

    public static CharacterTable FromJson(string jsonString)
    {
        CharacterTable characterTable = new CharacterTable();
        characterTable.characters = SaveSystem.LoadJson_String<Dictionary<string, Character>>(jsonString);

        foreach (var item in characterTable.characters)
        {
            item.Value.id = item.Key;
        }

        return characterTable;
    }

    public static Character GetCharacter(string uid)
    {
        if (!DatabaseManager.Instance.characterTable.characters.ContainsKey(uid))
            return null;

        return DatabaseManager.Instance.characterTable.characters[uid];
    }
}
