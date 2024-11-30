using System.Collections.Generic;
using UnityEngine;
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
            if (selectSlot == null)
                Close();

            Close();
        });
    }

    public override void Show()
    {
        base.Show();
        base.ShowAnimation();

        selectSlot = null;
        DeSelectSlotsAll();

        var characters = UserDataManager.Instance.userData.characters;
        foreach (var character in characters)
        {
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
