using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public string uid;
    public Transform container;
    public string type;

    public GameObject origin;
    public Queue<GameObject> objects = new();

    public void Create()
    {
        GameObject spawn = GameObject.Instantiate(origin);
        spawn.SetActive(false);
        spawn.transform.SetParent(container);
        spawn.transform.SetZeroLocalPositonAndRotation();

        objects.Enqueue(spawn);
    }

    public GameObject GetObject()
    {
        if (objects.Count <= 0)
            Create();

        GameObject target = objects.Dequeue();
        if (!target.activeSelf)
            target.SetActive(true);

        return target;
    }

    public void Relese(GameObject target)
    {
        if (objects.Contains(target))
            return;

        if (target.activeSelf)
            target.SetActive(false);

        objects.Enqueue(target);
    }
}

public class ObjectPoolManager : Singleton_Mono<ObjectPoolManager>
{
    public List<ObjectPool> initPoolList = new();
    public Dictionary<string, ObjectPool> pools = new();


    private void Start()
    {
        initPoolList.ForEach(e =>
        {
            CreatePool(e.uid, e.origin);
        });

        initPoolList.Clear();
    }

    public void CreatePool(string tag, GameObject target)
    {
        if(pools.ContainsKey(tag))
            return;

        ObjectPool pool = new ObjectPool()
        {
            uid = tag,
            origin = GameObject.Instantiate(target),
            container = new GameObject(tag).transform,
        };

        pool.origin.SetActive(false);

        pools[tag] = pool;
    }


    public GameObject Get(string tag)
    {
        if (!pools.ContainsKey(tag))
            return null;

        return pools[tag].GetObject();
    }

    public T GetC<T>(string tag)
    {
        return Get(tag).GetComponent<T>();
    }

    public void Relese(string tag, GameObject target)
    {
        if (!pools.ContainsKey(tag))
            return;

        pools[tag].Relese(target);
    }
}
