using UnityEngine;

// UI 화면을 관리하는 화면 객체 입니다.
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
    /// 명시적 호출 가능 - 생략시 Start에서 초기화됨
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
    /// 초기화 시 수행할 목표 재정의
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
