using Mono.Cecil;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : Singleton_Mono<DatabaseManager>
{
    public TextAsset jsonDataFile; 
    public TextAsset skillJsonText;
    public TextAsset itemJsonText;
    public TextAsset rangeJsonText;
    public TextAsset gacha_table_json;
    public TextAsset skin_table_json;
    public TextAsset resource_table_json;

    public Dictionary<string, SkillData> characterSkillDictionary = new();
    public Dictionary<string, ItemData> itemDictionary = new();
    public Dictionary<string, RangeInfo> rangeDictionary = new();

    public GachaTable gachaTable;
    public SkinTable skinTable;
    public SkinSearchTable skinSearchTable;
    public CharacterTable characterTable;
    public ResourceTable resourceTable;


    public override void Init()
    {
        base.Init();

        PaserRangeData();
        PaserItemData();
        PaserSkillData();

        characterTable = CharacterTable.FromJson(jsonDataFile.text);
        gachaTable = GachaTable.FromJson(gacha_table_json.text);
        skinTable = SkinTable.FromJson(skin_table_json.text);
        skinSearchTable = SkinSearchTable.FromData(skinTable);

        resourceTable = ResourceTable.FromJson(resource_table_json.text);
    }

    public void PaserSkillData()
    {
        if (skillJsonText != null)
        {
            // TextAsset의 텍스트 데이터를 읽어옵니다.
            string jsonData = skillJsonText.text;

            // Newtonsoft.Json을 사용하여 JSON 데이터를 Dictionary<string, SkillData>로 변환합니다.
            characterSkillDictionary = JsonConvert.DeserializeObject<Dictionary<string, SkillData>>(jsonData);

            // 특정 스킬 데이터에 접근합니다.
            if (characterSkillDictionary != null && characterSkillDictionary.ContainsKey("skcom_charge_cost[1]"))
            {
                SkillData skill = characterSkillDictionary["skcom_charge_cost[1]"];
                Debug.Log("Skill ID: " + skill.skillId);
                Debug.Log("Hidden: " + skill.hidden);

                // 각 레벨의 스킬 데이터를 출력합니다.
                foreach (var level in skill.levels)
                {
                    Debug.Log("Level Name: " + level.name);
                    Debug.Log("Description: " + level.description);
                    Debug.Log("SP Cost: " + level.spData.spCost);
                }
            }
            else
            {
                Debug.LogError("Skill data not found.");
            }
        }
        else
        {
            Debug.LogError("No JSON data file assigned to TextAsset.");
        }

        Debug.Log(characterSkillDictionary.Count);
    }

    public void PaserItemData()
    {
        if (itemJsonText != null)
        {
            // TextAsset의 텍스트 데이터를 읽어옵니다.
            string jsonData = itemJsonText.text;

            // Newtonsoft.Json을 사용하여 JSON 데이터를 Dictionary<string, ItemData>로 변환합니다.
            itemDictionary = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(jsonData);

            // 특정 아이템 데이터에 접근합니다.
            if (itemDictionary != null && itemDictionary.ContainsKey("4003"))
            {
                ItemData item = itemDictionary["4003"];
                Debug.Log("Item ID: " + item.itemId);
                Debug.Log("Name: " + item.name);
                Debug.Log("Description: " + item.description);

                // StageDropList 데이터를 출력합니다.
                if (item.stageDropList != null)
                {
                    foreach (var stageDrop in item.stageDropList)
                    {
                        Debug.Log("Stage ID: " + stageDrop.stageId);
                        Debug.Log("Occurrence: " + stageDrop.occPer);
                    }
                }
            }
            else
            {
                Debug.LogError("Item data not found.");
            }
        }
        else
        {
            Debug.LogError("No JSON data file assigned to TextAsset.");
        }

        Debug.Log(itemDictionary.Count);
    }

    public void PaserRangeData()
    {
        if (rangeJsonText != null)
        {
            // TextAsset의 텍스트 데이터를 읽어옵니다.
            string jsonData = rangeJsonText.text;

            // Newtonsoft.Json을 사용하여 JSON 데이터를 Dictionary<string, RangeInfo>로 변환합니다.
            rangeDictionary = JsonConvert.DeserializeObject<Dictionary<string, RangeInfo>>(jsonData);

            // 특정 범위 데이터에 접근합니다.
            if (rangeDictionary != null && rangeDictionary.ContainsKey("0-1"))
            {
                RangeInfo range = rangeDictionary["0-1"];
                Debug.Log("Range ID: " + range.id);
                Debug.Log("Direction: " + range.direction);

                // Grids 데이터를 출력합니다.
                foreach (var grid in range.grids)
                {
                    Debug.Log("Grid Position - Row: " + grid.row + ", Col: " + grid.col);
                }
            }
            else
            {
                Debug.LogError("Range data not found.");
            }

            // 다른 범위 데이터에 접근
            if (rangeDictionary.ContainsKey("1-1"))
            {
                RangeInfo range = rangeDictionary["1-1"];
                Debug.Log("Range ID: " + range.id);
                Debug.Log("Direction: " + range.direction);

                // Grids 데이터를 출력합니다.
                foreach (var grid in range.grids)
                {
                    Debug.Log("Grid Position - Row: " + grid.row + ", Col: " + grid.col);
                }
            }
        }
        else
        {
            Debug.LogError("No JSON data file assigned to TextAsset.");
        }

        Debug.Log(rangeDictionary.Count);
    }



#if UNITY_EDITOR
    [Button("Create Resource Table")]
    public void _Editor_CreateResourceTable()
    {
        CharacterTable characterTable = CharacterTable.FromJson(jsonDataFile.text);
        SkinTable skinTable = SkinTable.FromJson(skin_table_json.text);
        SkinSearchTable skinSearchTable = SkinSearchTable.FromData(skinTable);

        Dictionary<string, ResourceData> resourceTable = new();

        foreach(var character in characterTable.characters)
        {
            if (character.Key.Contains("trap_"))
                continue;
            if (character.Key.Contains("token_"))
                continue;

            SkinData elit0 = skinSearchTable.Search_Elite0(character.Key);

            resourceTable[elit0.avatarId] = new ResourceData()
            {
                key = elit0.avatarId,
                downloadUrl = $"avatars/{elit0.avatarId}.png",
                path = $"Assets/Avatars/{elit0.avatarId}.png",
            };

            resourceTable[elit0.portraitId] = new ResourceData()
            {
                key = elit0.portraitId,
                downloadUrl = $"portraits/{elit0.portraitId}.png",
                path = $"Assets/Portraits/{elit0.portraitId}.png",
            };

            resourceTable[elit0.illustId] = new ResourceData()
            {
                key = elit0.illustId,
                downloadUrl = $"characters/{elit0.illustId.Replace("illust_", "")}.png",
                path = $"Assets/Characters/{elit0.illustId.Replace("illust_", "")}.png",
            };

            SkinData elit2 = skinSearchTable.Search_Elite2(character.Key);
            if (elit2 == null)
                continue;

            resourceTable[elit2.illustId] = new ResourceData()
            {
                key = elit2.illustId,
                downloadUrl = $"characters/{elit2.illustId.Replace("illust_", "")}.png",
                path = $"Assets/Characters/{elit2.illustId.Replace("illust_", "")}.png",
            };
        }

        SaveSystem.SaveJson_AssetPath(resourceTable, "Database/resourceTable");
    }
#endif
}
