using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static StackManager Instance;
    public Transform stackHolder; // đối tượng cha chứa tất cả stack
    public Transform playerBody  ; 
    public float stackHeight = 0.5f ; 
    public List<GameObject> stackList = new List<GameObject>();    
    public int stackCount => stackList.Count;
    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Triggered: " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Stack")){
            stackList.Add(other.gameObject);
            other.transform.SetParent(stackHolder);
            other.transform.localPosition = new UnityEngine.Vector3(0 , stackHeight * (stackList.Count - 1)- 0.5f , 0) ;  
            playerBody.localPosition += new UnityEngine.Vector3(0 , stackHeight , 0) ;
        }
    }
    void OnTriggerExit(Collider other)
    {
        
    }
}
