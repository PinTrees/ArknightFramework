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
    /// 확정
    /// 씬을 런타임 중에 비동기 로드후 현재 씬에 추가합니다.
    /// Addressable을 사용하여 유니티에서 자체관리되는 메모리 시스템을 사용합니다.
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
    /// 테스트
    /// 런타임중 로드된 씬을 비동기 해제합니다.
    /// Addressable을 사용하여 유니티에서 자체관리되는 메모리 시스템을 사용합니다.
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
