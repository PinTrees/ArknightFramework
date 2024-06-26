using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIObjectBase : MonoBehaviour
{
    [SerializeField] public GameObject target;

    public RectTransform rectTransform { get; private set; }
    public Canvas canvas { get; private set; }

    [field: SerializeField]
    public UIObjectBase parent { get; private set; }

    [HideInInspector] 
    public List<UIObjectBase> children = new();

    public bool HasChildren() => children.Count > 0;
    public void SetRectTransform(RectTransform rectTransform) { this.rectTransform = rectTransform; }

    bool _init = false;
    bool _isInitializing = false;

    public bool isPointFocus = false;


    protected virtual void Awake()
    {
        Close();
    }

    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// ��������� ȣ������ ���� �� Start���� �ڵ����� ȣ���
    /// </summary>
    public void Init()
    {
        if (_isInitializing)
        {
            _init = true;
            _isInitializing = false;
            //Debug.Log("���� ��� ����.");
            return;
        }
        if (_init)
            return;

        if(target == null)
            target = this.gameObject;

        if (canvas == null)
            canvas = target.GetComponentInParent<Canvas>();

        if (rectTransform == null)
            rectTransform = target.GetComponent<RectTransform>();
        
        if(rectTransform == null)
            rectTransform = target.AddComponent<RectTransform>();

        OnInit();

        _init = true;
        _isInitializing = false;
    }

    /// <summary>
    /// ������ ���� ����ȭ �޼���
    /// </summary>
    protected virtual void OnInit()
    {
        _isInitializing = true;
    }


    public bool IsShowed()
    {
        if (target == null)
            return true;

        return !target.activeSelf;
    }


    public virtual void Show()
    {
        if (!_init)
            Init();

        if (target == null)
            return;

        target.SetActive(true);
    }

    public virtual void Close()
    {
        if (!_init)
            Init();

        if (target == null)
            return;

        target.SetActive(false);
        target.transform.SetParent(null, true);
    }

    public virtual void AddChildren(List<UIObjectBase> uis)
    {
        foreach(var ui in uis)
        {
            ui.transform.SetParent(transform, true);
            ui.transform.localScale = Vector3.one;
            ui.transform.localPosition = Vector3.zero;
            ui.parent = this;

            children.Add(ui);  
        }
    }
    public virtual void AddChild(UIObjectBase ui)
    {
        ui.transform.SetParent(transform, true);
        ui.transform.localScale = Vector3.one;
        ui.transform.localPosition = Vector3.zero;
        ui.parent = this;

        children.Add(ui);
    }

    protected void OnUpdateParent()
    {
        Init();

        if (target.transform.parent != null)
            parent = target.transform.parent.GetComponent<UIObjectBase>();
        else
            parent = null;
    }


    protected virtual void LateUpdate()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, null))
        {
            if (!isPointFocus)
            {
                isPointFocus = true;
                OnMouseRectEnter();
            }
        }
        else
        {
            if (isPointFocus)
            {
                isPointFocus = false;
                OnMouseRectExit();
            }
        }
    }

    protected virtual void OnMouseRectEnter()
    {
        //Debug.Log("Mouse entered the rect boundary");
    }

    protected virtual void OnMouseRectExit()
    {
        //Debug.Log("Mouse exited the rect boundary");
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(UIObjectBase), true)]
public class UIObjectBaseEditor : Editor
{
    UIObjectBase owner;

    public void OnEnable()
    {
        owner = target as UIObjectBase; 
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI(); 

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Show"))
        {
            owner.Show();
            Debug.Log("Show"); 
        }
        if (GUILayout.Button("Close"))
        {
            owner.Close();
            Debug.Log("Close");
        }

        GUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck()) 
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif