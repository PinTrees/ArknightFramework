using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_FormCharacterSelect_Slot : UIObjectBase
{
    [Header("UI Components Setting")]
    public RawImage characterPortailImage;
    public Text characterNameText;
    public Button characterIconButton;
    public GameObject selectFrameObject;
    public GameObject selectOverlayObject;

    [Header("Runtime Value")]
    [SerializeField] public UserCharacterData userCharacterData;

    protected override void OnInit()
    {
        base.OnInit();

        characterIconButton.onClick.RemoveAllListeners();
        characterIconButton.onClick.AddListener(() =>
        {
            UISystemManager.Instance.GetView<UI_FormCharacterSelect_View>().SetSelect(this);
        });
    }

    public void Show(UserCharacterData userCharacterData)
    {
        base.Show();

        characterPortailImage.texture = null;
        this.userCharacterData = userCharacterData;

        var characterDataObject = CharacterTable.GetCharacter(userCharacterData.char_uid);

        string path = ResourceManager.Instance.GetCharacterPortailResourceData(userCharacterData.char_uid, 0);
        characterPortailImage.texture = FileEx.GetPNG(path);
        characterNameText.text = characterDataObject.name;

        selectFrameObject.SetActive(false);
        selectOverlayObject.SetActive(false);
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnSelect(bool active)
    {
        selectFrameObject.SetActive(active);
        selectOverlayObject.SetActive(active);
    }
}
