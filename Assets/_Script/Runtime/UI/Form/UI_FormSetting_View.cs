using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UI_FormSetting_View : UIViewBase
{
    [Header("Transform Setting")]
    public Transform slotContainerTrasnform;

    [Header("UI Components Setting")]
    public Button formGroupRightArrowButton;
    public Button formGropLeftArrowButton;
    public UI_SelectButtonGroup formSelectGroup;

    [Header("Runtime Value")]
    public List<UI_FormCharacter_Slot> slots = new();
    public UserTeamFormationData currentUserTeamFormationData;

    protected override void OnInit()
    {
        base.OnInit();

        formGroupRightArrowButton.onClick.RemoveAllListeners();
        formGropLeftArrowButton.onClick.RemoveAllListeners();

        formGroupRightArrowButton.onClick.AddListener(() =>
        {
            formSelectGroup.SetSelectNext();
        });
        formGropLeftArrowButton.onClick.AddListener(() =>
        {
            formSelectGroup.SetSelectPre();
        });
    }

    public override void Show()
    {
        base.Show();
        base.ShowAnimation();

        currentUserTeamFormationData = null;
        formSelectGroup.Show();

        var userTeamFormaitionSettings = UserDataManager.Instance.userData.teamFormations;
        currentUserTeamFormationData = userTeamFormaitionSettings.Where(e => e.index == formSelectGroup.currentSelectIndex).FirstOrDefault();
        if(currentUserTeamFormationData == null)
        {
            currentUserTeamFormationData = new();
        }

        ShowCharacterSlots();

        UISystemManager.Instance.GetView<UI_TopTab>().AddUndo(() =>
        {
            Close();
        }).Show();
    }

    public override void Refresh()
    {
        base.Refresh();

        ClearCharacterSlots();
        ShowCharacterSlots();
    }

    private void ShowCharacterSlots()
    {
        for (int i = 0; i < 12; ++i)
        {
            var slot = UISystemManager.Instance.Create<UI_FormCharacter_Slot>("UI_FormCharacter_Slot");
            slot.transform.SetParent(slotContainerTrasnform, true);

            if (i >= currentUserTeamFormationData.characters.Count)
            {
                slot.Show();
                slots.Add(slot);
            }
            else
            {
                slot.Show(currentUserTeamFormationData.characters[i]);
                slots.Add(slot);
            }
        }
    }
    private void ClearCharacterSlots()
    {
        slots.ForEach(e =>
        {
            UISystemManager.Instance.Release(e.gameObject);
        });
        slots.Clear();
    }

    
    public override void Close()
    {
        base.CloseAnimation(() =>
        {
            base.Close();
            ClearCharacterSlots();
        });

        UserDataManager.Instance.SaveData();
    }

    public void AddTeamCharacter(UserCharacterData userCharacter)
    {
        if(userCharacter == null)
        {
            // 坷幅贸府
            return;
        }

        currentUserTeamFormationData.characters.Add(userCharacter);
        Refresh();
        UISystemManager.Instance.GetView<UI_TopTab>().Pop();
        UserDataManager.Instance.SaveData();
    }
    public void ReplaceTeamCharacter(UserCharacterData oldCharacter, UserCharacterData newCharacter)
    {
        if (oldCharacter == null || newCharacter == null)
        {
            // 坷幅贸府
            return;
        }

        var index = currentUserTeamFormationData.characters.IndexOf(oldCharacter);
        currentUserTeamFormationData.characters.RemoveAt(index);
        currentUserTeamFormationData.characters.Insert(index, newCharacter);
        Refresh();
        UISystemManager.Instance.GetView<UI_TopTab>().Pop();
        UserDataManager.Instance.SaveData();
    }
}
