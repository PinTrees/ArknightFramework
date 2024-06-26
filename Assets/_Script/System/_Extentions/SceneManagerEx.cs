using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_ADDRESSABLEASSETS
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
#endif

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public static class SceneManagerEx
{
#if UNITY_ADDRESSABLEASSETS
    /// <summary>
    /// Ȯ��
    /// ���� ��Ÿ�� �߿� �񵿱� �ε��� ���� ���� �߰��մϴ�.
    /// Addressable�� ����Ͽ� ����Ƽ���� ��ü�����Ǵ� �޸� �ý����� ����մϴ�.
    /// </summary>
    /// <param name="fullpath"></param>
    /// <param name="complete"></param>
    public static void LoadSceneAsync_Addressables(string fullpath, Action<AsyncOperationHandle<SceneInstance>> complete = null)
    {
        var handle = Addressables.LoadSceneAsync(fullpath, LoadSceneMode.Additive, true);
        if (complete != null)
            handle.Completed += complete;
    }
#endif

#if UNITY_ADDRESSABLEASSETS
    /// <summary>
    /// �׽�Ʈ
    /// ��Ÿ���� �ε�� ���� �񵿱� �����մϴ�.
    /// Addressable�� ����Ͽ� ����Ƽ���� ��ü�����Ǵ� �޸� �ý����� ����մϴ�.
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="complete"></param>
    public static void UnloadSceneAsync_Addressables(SceneInstance? instance, Action<AsyncOperationHandle<SceneInstance>> complete = null)
    {
        if (instance == null)
            return;

        var handle = Addressables.UnloadSceneAsync(instance.Value, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        if (complete != null)
            handle.Completed += complete;
    }
#endif
}
