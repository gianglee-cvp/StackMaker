using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public string tag;
    public GameObject prefab;
}
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public void OnInit()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

    }
    
    
}