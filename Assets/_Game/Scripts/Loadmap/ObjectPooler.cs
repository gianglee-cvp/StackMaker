using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private Dictionary<MapGenTag, Queue<PoolObject>> poolDictionary;

    public static ObjectPooler Instance;

    public void OnInit()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        poolDictionary = new Dictionary<MapGenTag, Queue<PoolObject>>();
    }

    public PoolObject SpawnFromPool(PoolObject objPrefab, MapGenTag tag, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            poolDictionary.Add(tag, new Queue<PoolObject>());
        }
        if (poolDictionary[tag].Count == 0)
        {
            PoolObject obj = Instantiate(objPrefab, position, rotation, parent);
            obj.OnSpawn();
            return obj;
        }
        else
        {
            PoolObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.trans.SetParent(parent);
            objectToSpawn.trans.position = position;
            objectToSpawn.trans.rotation = rotation;
            objectToSpawn.gameObject.SetActive(true);
            if (objectToSpawn.obj != null) objectToSpawn.obj.SetActive(true);
            objectToSpawn.OnSpawn();
            return objectToSpawn;
        }
    }

    public void ReturnToPool(MapGenTag tag, PoolObject obj)
    {
        obj.OnDespawn();
    }

    public void ReturnObjToQueue(MapGenTag tag, PoolObject obj)
    {
        poolDictionary[tag].Enqueue(obj);
    }
}