using UnityEngine;
using UnityEngine.UI;

public class UI_Cheet_CharacterSearchHeader : UIObjectBase
{
    public RawImage characterIconImage;
    public Text characterNameText;
    public Button characterGetButton;

    public Character character;


    protected override void OnInit()
    {
        base.OnInit();

        characterGetButton.onClick.RemoveAllListeners();
        characterGetButton.onClick.AddListener(() =>
        {
            UserDataManager.Instance.GetCharacter(character);
        });
    }

    public override void Show()
    {
        base.Show();

        if (character == null)
            return;

        characterIconImage.texture = FileEx.GetPNG(ResourceManager.Instance.GetCharacterIconResourceData(character.id, 0));
        characterNameText.text = character.name;
    }

    public override void Close()
    {
        base.Close();

        character = null;
        characterIconImage.texture = null;
        ObjectPoolManager.Instance.Release(ObjectPoolTags.UI_Cheet_CharacterSearchHeader, this.gameObject);
    }
}
