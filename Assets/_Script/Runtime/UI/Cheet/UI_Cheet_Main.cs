using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cheet_Main : UIViewBase
{
    public InputField characterSearchField;
    public Button characterSearchButton;

    public Transform contentsTransform;
    public List<UI_Cheet_CharacterSearchHeader> characterSearchList = new();


    protected override void OnInit()
    {
        base.OnInit();
        Close();

        characterSearchButton.onClick.RemoveAllListeners();
        characterSearchButton.onClick.AddListener(() =>
        {
            characterSearchList.ForEach(e => { e.Close(); });
            characterSearchList.Clear();

            var characters = DatabaseManager.Instance.characterTable.characters.Where(e =>
            {
                return e.Value.name.Contains(characterSearchField.text);
            }).ToList();

            characters.ForEach(e =>
            {
                var characterSearchHeader = ObjectPoolManager.Instance.GetC<UI_Cheet_CharacterSearchHeader>(ObjectPoolTags.UI_Cheet_CharacterSearchHeader);
                characterSearchHeader.character = e.Value;
                characterSearchHeader.transform.SetParent(contentsTransform, true);
                characterSearchHeader.Show();
                characterSearchList.Add(characterSearchHeader);
            });
        });
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Close()
    {
        base.Close();
    }
}
