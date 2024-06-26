using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public static class ListEx
{
    public static T GetRandomElement<T>(this List<T> list) where T : class
    {
        return list[Random.Range(0, list.Count)];
    }

    public static void Clear<T>(this List<T> list, Action<T> action) where T : class
    {
        for(int i = 0; i < list.Count; ++i)
        {
            if (list[i] == null)
                continue;

            // Unity�� �����ϴ� ��ü�� �̹� �����Ǿ����� �˻�
            if (list[i] is UnityEngine.Object obj && obj == null)
                continue;

            action(list[i]);
        }
        list.Clear();
    }

    public static bool IsEmpty<T>(this List<T> list)
    {
        if (list == null)
            return true;
        if (list.Count <= 0)
            return true;

        return false;
    }
}
