using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterInfo_View : UIViewBase
{
    // component
    public RawImage characterIllustImage;
    public Text characterNameText;
    public Text characterEnNameText;

    // runtime value
    public UserCharacterData userCharacterData;
    public Character character;


    protected override void OnInit()
    {
        base.OnInit();
        Close();
    }

    public override void Show()
    {
        base.Show();

        if (userCharacterData == null)
            return;
        if (character == null)
            return;

        string path = ResourceManager.Instance.GetCharacterIllustResourceData(userCharacterData.char_uid, 0);
        characterIllustImage.texture = FileEx.GetPNG(path);

        if (characterIllustImage.texture != null)
            characterIllustImage.enabled = true;
        else characterIllustImage.enabled = false;

        characterNameText.text = character.name;
        characterEnNameText.text = character.appellation;

        var hudView = UIManager.Instance.GetView<UI_Hud_View>();
        hudView.AddBackAction(() => Close());
        hudView.Show();
    }

    public override void Close()
    {
        base.Close();
        characterIllustImage.texture = null;
    }
}
