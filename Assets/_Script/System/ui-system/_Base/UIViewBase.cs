using UnityEngine;

// UI ȭ���� �����ϴ� ȭ�� ��ü �Դϴ�.
public class UIViewBase : MonoBehaviour
{
    public Canvas canvas { get; private set; }
    public GameObject target { get; private set; }
    public RectTransform rectTransform { get; private set; }

    private bool _init = false;


    protected virtual void Awake()
    {
        UIManager.Instance.AddView(this);
    }

    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// ����� ȣ�� ���� - ������ Start���� �ʱ�ȭ��
    /// </summary>
    public void Init()
    {
        if (_init)
            return;

        if (target == null)
            target = this.gameObject;

        rectTransform = target.transform as RectTransform;

        OnInit();

        _init = true;
    }

    /// <summary>
    /// �ʱ�ȭ �� ������ ��ǥ ������
    /// </summary>
    protected virtual void OnInit()
    {
    }

    public bool IsShow()
    {
        return target.activeSelf;
    }

    public virtual void Show()
    {
        if (target.activeSelf)
            return;

        target.SetActive(true);
    }  

    public virtual void Close()
    {
        if (!target.activeSelf)
            return;

        target.SetActive(false);
    }


    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }
}
