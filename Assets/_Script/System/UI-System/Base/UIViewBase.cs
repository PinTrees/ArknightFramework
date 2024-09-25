using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

// UI 화면을 관리하는 화면 객체 입니다.
public class UIViewBase : MonoBehaviour
{
    public Canvas canvas { get; private set; }
    [field: SerializeField]
    public GameObject baseObject { get; private set; }
    public RectTransform rectTransform { get; private set; }

    [HideInInspector]
    public CanvasGroup canvasGroup;

    private bool _init = false;


    protected virtual void Awake()
    {

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

        _init = true;

        if (baseObject == null)
            baseObject = this.gameObject;

        rectTransform = baseObject.transform as RectTransform;

        OnInit();
    }

    /// <summary>
    /// 초기화 시 수행할 목표 재정의
    /// </summary>
    protected virtual void OnInit()
    {
        canvasGroup = baseObject.AddComponent<CanvasGroup>();
    }

    public bool IsShow()
    {
        Init();

        return baseObject.activeSelf;
    }

    public virtual async UniTask<object> ShowWaitClose()
    {
        await UniTask.Yield();
        return null;
    }

    public virtual void Show()
    {
        Init();

        if (baseObject.activeSelf)
            return;

        baseObject.SetActive(true);
    }  

    public virtual void Refresh()
    {

    }

    public void ShowAnimation()
    {
        if (canvasGroup == null)
            return;

        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.3f);
    }

    public virtual void Close()
    {
        Init();

        if (!baseObject.activeSelf)
            return;


        baseObject.SetActive(false);
    }

    public virtual void CloseAnimation(Action onComplete = null)
    {
        Init();

        if (canvasGroup == null)
        {
            return;
        }

        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0.01f, 0.3f).OnComplete(() =>
        {
            if (onComplete != null)
                onComplete();
        });
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }
}
