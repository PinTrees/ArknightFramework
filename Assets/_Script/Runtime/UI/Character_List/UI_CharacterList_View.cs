using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterList_View : UIViewBase
{
    public Transform contentsTransform;
    public List<UI_Character_Slot> slots = new();


    protected override void OnInit()
    {
        base.OnInit();
    }

    public override void Show()
    {
        base.Show();
        base.ShowAnimation();

        var characters = UserDataManager.Instance.userData.characters;
        foreach (var character in characters)
        {
            UI_Character_Slot slot = ObjectPoolManager.Instance.GetC<UI_Character_Slot>(ObjectPoolTags.UI_Character_Slot);
            slot.transform.SetParent(contentsTransform, true);
            slot.userCharacterData = character;
            slot.Show();
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

            slots.ForEach(slot =>
            {
                ObjectPoolManager.Instance.Release(ObjectPoolTags.UI_Character_Slot, slot.gameObject);
            });
            slots.Clear();
        });
    }
}
