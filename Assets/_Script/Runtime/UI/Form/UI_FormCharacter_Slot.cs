using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_FormCharacter_Slot : UIObjectBase
{
    [Header("UI Container Setting")]
    public GameObject levelContainer;
    public GameObject classIconContainer;
    public UI_StarContainer starsContainer;

    [Header("UI Components Setting")]
    public Button button;
    public Button characterIconButton;
    public RawImage characterPortailImage;
    public Text characterNameText;
    public Image rareAuraImage;
    public Image rareAuraImage2;
    public Image rareBackgroundImage;
    public Text characterLevelText;
    public Image classIconImage;

    public UserCharacterData userCharacterData;


    protected override void OnInit()
    {
        base.OnInit();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            ShowFormCharacterSelectView();
        });
    }

    private async void ShowFormCharacterSelectView()
    {
        var currentSelectCharacters = UISystemManager.Instance.GetView<UI_FormSetting_View>().currentUserTeamFormationData.characters;

        var selectCharacter = await UISystemManager.Instance.GetView<UI_FormCharacterSelect_View>().ShowWaitClose(
            userCharacterData, currentSelectCharacters) as UserCharacterData;
        if (selectCharacter != null)
        {
            if(userCharacterData != null)
            {
                UISystemManager.Instance.GetView<UI_FormSetting_View>().ReplaceTeamCharacter(userCharacterData, selectCharacter);
            }
            else
            {
                UISystemManager.Instance.GetView<UI_FormSetting_View>().AddTeamCharacter(selectCharacter);
            }
        }
    }
    public override void Show()
    {
        base.Show();

        userCharacterData = null;

        characterPortailImage.gameObject.SetActive(false);
        characterNameText.gameObject.SetActive(false);
        rareAuraImage.gameObject.SetActive(false);
        rareAuraImage2.gameObject.SetActive(false);

        rareBackgroundImage.sprite = ResourceManager.Instance.GetCharacterRareBackgroundSprite("TIER_1");

        levelContainer.SetActive(false);
        classIconContainer.SetActive(false);
        starsContainer.Close();
    }

    public void Show(UserCharacterData userCharacterData)
    {
        base.Show();

        this.userCharacterData = userCharacterData;

        var character = DatabaseManager.Instance.characterTable.characters[userCharacterData.char_uid];
        characterNameText.text = character.name;
        characterNameText.gameObject.SetActive(true);

        string path = ResourceManager.Instance.GetCharacterPortailResourceData(userCharacterData.char_uid, 0);
        characterPortailImage.texture = FileEx.GetPNG(path);
        characterPortailImage.gameObject.SetActive(true);

        rareAuraImage.sprite = ResourceManager.Instance.GetCharacterRareAureSprite(character.rarity);
        rareAuraImage.gameObject.SetActive(true);
        rareBackgroundImage.sprite = ResourceManager.Instance.GetCharacterRareBackgroundSprite(character.rarity);
        rareBackgroundImage.gameObject.SetActive(true);

        rareAuraImage2.color = ResourceManager.Instance.GetCharacterRareAureColor(character.rarity);
        rareAuraImage2.gameObject.SetActive(true);

        characterLevelText.text = userCharacterData.level.ToString();
        levelContainer.SetActive(true);

        classIconImage.sprite = ResourceManager.Instance.GetClassIcon(character.profession);
        classIconContainer.SetActive(true);

        starsContainer.Show(int.Parse(character.rarity.Split("_").Last()));
    }

    public override void Close()
    {
        base.Close();
        userCharacterData = null;
    }
}
