using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextUI : UIObjectBase
{
    [TextArea(4, 4)]
    public string text = "NEW TEXT";
    public Font font;

    public Text textComponent;
    public Color textColor = Color.black;
    public int fontSize = 24;
    

    private void Reset()
    {
        Init();
    }

    protected override void OnInit()
    {
        base.OnInit();

        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        textComponent = target.GetOrAddComponent<Text>();
        textComponent.font = null;
    }

    public void Refresh()
    {
        if(textComponent == null)
        {
            return;
        }

        textComponent.text = text;
        textComponent.color = textColor;
        textComponent.fontSize = fontSize;
        textComponent.fontStyle = FontStyle.Bold;
        textComponent.font = font;

        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        textComponent.verticalOverflow = VerticalWrapMode.Overflow;

        UpdateLayout();
    }

    private void UpdateLayout()
    {
        // �ؽ�Ʈ�� ���� ������ ũ�⸦ ��� ���� cachedTextGenerator�� ���
        TextGenerationSettings settings = textComponent.GetGenerationSettings(rectTransform.rect.size);
        float width = textComponent.cachedTextGeneratorForLayout.GetPreferredWidth(textComponent.text, settings) / textComponent.pixelsPerUnit;
        float height = textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(textComponent.text, settings) / textComponent.pixelsPerUnit;

        // RectTransform�� ũ�⸦ �ؽ�Ʈ�� ���� ũ��� ����
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    private void OnValidate()
    {
        Init();
        Refresh();
    }
}
