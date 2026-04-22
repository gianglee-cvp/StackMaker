using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    private Dictionary<MapGenTag , Queue<GameObject>> poolDictionary ;

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
            Debug.Log("Instantiated new object for tag: " + tag);
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
    public void ReturnToPool(MapGenTag tag , GameObject obj)
    {
        if(tag == MapGenTag.Bridge)
        {
            obj.GetComponent<BoxCollider>().enabled = true ;
                obj.transform.GetChild(0).gameObject.SetActive(false) ;
            }
        obj.transform.SetParent(null) ;
        obj.SetActive(false) ; 
        poolDictionary[tag].Enqueue(obj) ; 
    }

    
}