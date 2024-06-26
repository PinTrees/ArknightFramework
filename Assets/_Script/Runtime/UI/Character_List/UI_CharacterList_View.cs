using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterList_View : UIViewBase
{
    public Transform contentsTransform;
    public List<UI_Character_Slot> slots = new();


    protected override void OnInit()
    {
        base.OnInit();
        Close();
    }

    public override void Show()
    {
        base.Show();

        var characters = UserDataManager.Instance.userData.characters;
        foreach (var character in characters)
        {
            UI_Character_Slot slot = ObjectPoolManager.Instance.GetC<UI_Character_Slot>(ObjectPoolTags.UI_Character_Slot);
            slot.transform.SetParent(contentsTransform, true);
            slot.userCharacterData = character;
            slot.Show();
            slots.Add(slot);
        }

        var hudView = UIManager.Instance.GetView<UI_Hud_View>();
        hudView.AddBackAction(() => Close());
        hudView.Show();
    }

    public override void Close()
    {
        base.Close();

        slots.ForEach(slot =>
        {
            ObjectPoolManager.Instance.Relese(ObjectPoolTags.UI_Character_Slot, slot.gameObject);
        });
        slots.Clear();
    }
}
