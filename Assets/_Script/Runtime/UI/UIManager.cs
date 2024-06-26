using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton_Mono<UIManager>
{
    public Dictionary<string, UIViewBase> views = new();

    public void AddView(UIViewBase view)
    {
        views[TypeOf.GetLastType(view).FullName] = view;
    }

    public T GetView<T>() where T : UIViewBase
    {
        return views[typeof(T).FullName] as T;
    }
}
