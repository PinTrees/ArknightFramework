using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore.Text;

public class ResourceManager : Singleton_Mono<ResourceManager>
{
    public static string baseUrl = "https://raw.githubusercontent.com/PinTrees/Arknight-Images/main/";

    [Header("headhunt resource")]
    public Sprite headhuntTitleSprite;
    public Sprite headhuntResum_6rare_Sprite;
    public Sprite headhuntResum_5rare_Sprite;
    public Sprite headhuntResum_4rare_Sprite;
    public Sprite headhuntResum_3rare_Sprite;
    public Sprite headhuntResum_2rare_Sprite;
    public Sprite headhuntResum_1rare_Sprite;

    [Header("classes resource")]
    public Sprite caster;
    public Sprite defender;
    public Sprite guard;
    public Sprite medic;
    public Sprite sniper;
    public Sprite specialist;
    public Sprite supporter;
    public Sprite vanguard;

    [ProgressBar]
    public float downloadProgress = 0f;

    int totalResources;
    int downloadedResources;
    private SemaphoreSlim semaphore = new SemaphoreSlim(100);  // ���� ���� �۾� ���� 100���� ����


    public override void Init()
    {
        //DownLoadResource();
    }

    // �ٿ�ε� �� ���� �Լ�
    public async void DownLoadResource()
    {
        ResourceTable resourceTable = DatabaseManager.Instance.resourceTable;

        totalResources = resourceTable.resources.Count; 
        downloadedResources = 0; 

        List<UniTask> downloadTasks = new List<UniTask>();

        foreach (var resource in resourceTable.resources.Values)
        {
            string url = baseUrl + resource.downloadUrl;
            string path = Path.Combine(Application.persistentDataPath, resource.path);

            // ������ �̹� �����ϴ��� Ȯ��
            if (File.Exists(path))
            {
                // Debug.Log($"File already exists at {path}, skipping download.");
                downloadedResources++;
                UpdateProgress(totalResources, downloadedResources);
                continue;
            }

            // SemaphoreSlim�� ����Ͽ� ���� �۾� �� ����
            await semaphore.WaitAsync();
            downloadTasks.Add(DownloadAndSaveFile(url, path, resource));
        }

        // ��� �ٿ�ε� �۾� �Ϸ� ���
        await UniTask.WhenAll(downloadTasks);
        Debug.Log("All resources have been processed.");
    }

    private async UniTask DownloadAndSaveFile(string url, string path, ResourceData resource)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            var operation = webRequest.SendWebRequest();

            // �ٿ�ε尡 �Ϸ�� ������ ���
            while (!operation.isDone)
            {
                await UniTask.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error downloading file from {url}: {webRequest.error}");

                downloadedResources++;
                UpdateProgress(totalResources, downloadedResources);
            }
            else
            {
                // �ٿ�ε�� ������ ���� ��ο� ����
                try
                {
                    // ������ ���丮�� ������ ����
                    string directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // ������ ����
                    File.WriteAllBytes(path, webRequest.downloadHandler.data);

                    // �ٿ�ε�� ������ ���� ��� ������Ʈ
                    resource.path = path;

                    Debug.Log($"File downloaded and saved to {path}");

                    downloadedResources++;
                    UpdateProgress(totalResources, downloadedResources);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error saving file to {path}: {e.Message}");

                    downloadedResources++;
                    UpdateProgress(totalResources, downloadedResources);
                }
            }
        }

        // SemaphoreSlim ī��Ʈ�� �������� ���� �۾��� ����� �� �ֵ��� ��
        semaphore.Release();
    }

    private void UpdateProgress(int totalResources, float currentProgress)
    {
        // ���� �ٿ�ε� ���� ��Ȳ�� ��ü ���ҽ� ���� ���� ������� ���
        downloadProgress = (currentProgress / totalResources);
    }



    public Sprite GetClassIcon(string classType)
    {
        if (classType == "MEDIC")
            return medic;
        else if (classType == "CASTER")
            return caster;
        else if (classType == "TANK")
            return defender;
        else if (classType == "WARRIOR")
            return guard;
        else if (classType == "SNIPER")
            return sniper;
        else if (classType == "SPECIAL")
            return specialist;
        else if (classType == "SUPPORT")
            return supporter;
        else if (classType == "PIONEER")
            return vanguard;

        return vanguard;
    }



    public string GetCharacterIconResourceData(string char_id, int elit)
    {
        SkinData skinData = null;

        if (elit == 0)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite0(char_id);
        else if (elit == 1)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite1(char_id);
        else if (elit == 2)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite2(char_id);

        return $"Assets/Avatars/{skinData.avatarId}.png";
    }

    public string GetCharacterIllustResourceData(string char_id, int elit)
    {
        SkinData skinData = null;

        if (elit == 0)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite0(char_id);
        else if (elit == 1)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite1(char_id);
        else if (elit == 2)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite2(char_id);

        return $"Assets/Characters/{skinData.illustId.Replace("illust_", "")}.png";
    }

    public string GetCharacterPortailResourceData(string char_id, int elit)
    {
        SkinData skinData = null;

        if (elit == 0)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite0(char_id);
        else if (elit == 1)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite1(char_id);
        else if (elit == 2)
            skinData = DatabaseManager.Instance.skinSearchTable.Search_Elite2(char_id);

        return $"Assets/Portraits/{skinData.portraitId}.png";
    }
}
