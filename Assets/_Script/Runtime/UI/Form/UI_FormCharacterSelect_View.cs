using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_FormCharacterSelect_View : UIViewBase
{
    [Header("Transform Setting")]
    public Transform slotContainerTransform;

    [Header("UI Components Setting")]
    public Button okButton; 

    [Header("Runtime Value")]
    [SerializeField] private List<UI_FormCharacterSelect_Slot> slots = new();
    [SerializeField] private UI_FormCharacterSelect_Slot selectSlot;

    protected override void OnInit()
    {
        base.OnInit();

        slotContainerTransform.DetachChildren();
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(() =>
        {
            Close();
        });
    }

    public async UniTask<object> ShowWaitClose(UserCharacterData currentSelectCharacter, List<UserCharacterData> hideSelectableCharacters=null)
    {
        await base.ShowWaitClose();
        Show(currentSelectCharacter, hideSelectableCharacters);

        while (baseObject.activeSelf)
        {
            await UniTask.Yield();
        }

        if (selectSlot != null)
        {
            return selectSlot.userCharacterData;
        }
        return null;
    }

    public void Show(UserCharacterData currentSelectCharacter, List<UserCharacterData> hideSelectableCharacters = null)
    {
        base.Show();
        base.ShowAnimation();

        selectSlot = null;
        DeSelectSlotsAll();

        if(currentSelectCharacter != null)
        {
            UI_FormCharacterSelect_Slot slot = ObjectPoolManager.Instance.GetC<UI_FormCharacterSelect_Slot>(ObjectPoolTags.UI_FormCharacterSelect_Slot);
            slot.transform.SetParent(slotContainerTransform, true);
            slot.Show(currentSelectCharacter);
            slots.Add(slot);
            SetSelect(slot);
        }

        var characters = UserDataManager.Instance.userData.characters;
        foreach (var character in characters)
        {
            if(currentSelectCharacter != null)
            {
                if (character.char_uid == currentSelectCharacter.char_uid)
                    continue;
            }

            if (hideSelectableCharacters.FirstOrDefault(e => e.char_uid == character.char_uid) != null)
                continue;

             UI_FormCharacterSelect_Slot slot = ObjectPoolManager.Instance.GetC<UI_FormCharacterSelect_Slot>(ObjectPoolTags.UI_FormCharacterSelect_Slot);
            slot.transform.SetParent(slotContainerTransform, true);
            slot.Show(character);
            slots.Add(slot);
        }

        UISystemManager.Instance.GetView<UI_TopTab>().AddUndo(() =>
        {
            Close();
        }).Show();
    }

    public override void Close()
    {
        base.CloseAnimation(() =>
        {
            base.Close();
            slots.ForEach(e =>
            {
                UISystemManager.Instance.Release(e.baseObject);
            });
        });
    }

    private void DeSelectSlotsAll()
    {
        slots.ForEach(e =>
        {
            e.OnSelect(false);
        });
    }

    public void SetSelect(UI_FormCharacterSelect_Slot slot)
    {
        DeSelectSlotsAll();
        
        selectSlot = slot;
        selectSlot.OnSelect(true);
    }
}
