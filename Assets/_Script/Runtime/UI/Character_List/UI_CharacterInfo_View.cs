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
        base.ShowAnimation();

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
            characterIllustImage.texture = null;
        });
    }
}
