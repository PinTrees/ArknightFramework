using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_Character_Slot : UIObjectBase
{
    public Button characterInfoViewButton;
    public RawImage characterPortailImage;
    public Image classIconImage;
    public Text characterNameText;

    // runtime value
    public UserCharacterData userCharacterData;
    public Character character;


    protected override void OnInit()
    {
        base.OnInit();

        characterInfoViewButton.onClick.RemoveAllListeners();
        characterInfoViewButton.onClick.AddListener(() =>
        {
            var view = UIManager.Instance.GetView<UI_CharacterInfo_View>();
            view.character = character;
            view.userCharacterData = userCharacterData;
            view.Show();
        });
    }

    public override void Show()
    {
        base.Show();

        if (userCharacterData == null)
            return;

        character = DatabaseManager.Instance.characterTable.characters[userCharacterData.char_uid];
        characterNameText.text = character.name;

        string path = ResourceManager.Instance.GetCharacterPortailResourceData(userCharacterData.char_uid, 0);
        characterPortailImage.texture = FileEx.GetPNG(path);

        if (characterPortailImage.texture == null)
            characterPortailImage.enabled = false;
        else characterPortailImage.enabled = true;

        classIconImage.sprite = ResourceManager.Instance.GetClassIcon(character.profession);
    }

    public override void Close()
    {
        base.Close();
        userCharacterData = null;
        characterPortailImage.texture = null;
    }
}
