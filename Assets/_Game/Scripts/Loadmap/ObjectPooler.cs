using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    private Dictionary<MapGenTag , Queue<GameObject>> poolDictionary ;
    private Dictionary<MapGenTag , Queue<PoolObject>> poolDictionaryTest ;

    public static ObjectPooler Instance  ; 

    public void OnInit()
    {
        if(Instance == null)
        {
            Instance = this ; 
        }
        else
        {
            Destroy(gameObject) ; 
        }
        poolDictionary = new Dictionary<MapGenTag, Queue<GameObject>>() ;
        poolDictionaryTest = new Dictionary<MapGenTag, Queue<PoolObject>>() ;


    }
    public void SpawnFromPool(GameObject objPrefab, MapGenTag tag , Vector3 position , Quaternion rotation , Transform parent )
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            poolDictionary.Add(tag , new Queue<GameObject>()) ;
        }
        if(poolDictionary[tag].Count == 0)
        {
            GameObject obj = Instantiate(objPrefab , position , rotation , parent) ; 
        }
        else
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue() ; 

            objectToSpawn.transform.SetParent(parent) ;
            objectToSpawn.transform.position = position ; 
            objectToSpawn.transform.rotation = rotation ; 
            objectToSpawn.SetActive(true) ; 
        }
    }
    public void ReturnToPool(MapGenTag tag ,PoolObject obj)
    {
        obj.OnDespawn() ;
    }
    public PoolObject SpawnFromPool(PoolObject objPrefab, MapGenTag tag , Vector3 position , Quaternion rotation , Transform parent )
    {
        if(!poolDictionaryTest.ContainsKey(tag))
        {
            poolDictionaryTest.Add(tag , new Queue<PoolObject>()) ;
        }
        if(poolDictionaryTest[tag].Count == 0)
        {
            PoolObject obj = Instantiate(objPrefab , position , rotation , parent) ; 
            obj.OnSpawn() ;
            return obj;
        }
        else
        {
            PoolObject objectToSpawn = poolDictionaryTest[tag].Dequeue() ; 

            objectToSpawn.trans.SetParent(parent) ;
            objectToSpawn.trans.position = position ; 
            objectToSpawn.trans.rotation = rotation ; 
            objectToSpawn.obj.SetActive(true) ;
            objectToSpawn.OnSpawn() ;
            return objectToSpawn ; 
        }
    }

    public void ReturnObjToQueue(MapGenTag tag , PoolObject obj)
    {
        poolDictionaryTest[tag].Enqueue(obj) ; 
    }
    
}