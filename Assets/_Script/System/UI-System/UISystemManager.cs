using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UISystemManager : Singleton<UISystemManager>  
{ 
    public Canvas canvas { get; private set; }
    
    public List<GameObject> uiObjects = new();      // Object Pool UI Parts - Manual Setting
    private List<UIViewBase> uiViews = new();       // All Views - Auto Setting

    // Font Setting
    [Space]
    public Font fontBase;


    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public override void Init()
    {
        base.Init();

        canvas = GameObject.FindAnyObjectByType<Canvas>();
        uiViews = Resources.FindObjectsOfTypeAll<UIViewBase>().ToList();

        uiObjects.ForEach(e =>
        {
            ObjectPoolManager.Instance.CreatePool(e.name, e.gameObject);
        });

        uiObjects.Clear();
    }

    #region ObjectPool
    public T Create<T>(string tag) where T : UIObjectBase
    {
        return ObjectPoolManager.Instance.GetC<T>(tag);
    }
    public void Release(GameObject uiObject)
    {
        ObjectPoolManager.Instance.Release(uiObject.name, uiObject.gameObject);
    }
    #endregion

    public T GetUI<T>() where T : class
    {
        foreach (var obj in uiObjects)
        {
            if (obj is T ui)
                return ui;
        }
        return null;
    }

    public T GetView<T>() where T : class
    {
        foreach (var obj in uiViews)
        {
            if (obj is T ui)
                return ui;
        }
        return null;
    }
}
