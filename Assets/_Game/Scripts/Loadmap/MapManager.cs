using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;

public partial class MapManager : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private GameObject basePrefab ;
    [SerializeField] private GameObject wallPrefab ;
    [SerializeField] private GameObject stackPrefab ;
    [SerializeField] private GameObject cornerPrefab ;
    [SerializeField] private GameObject bridgePrefab ;
    [SerializeField] private GameObject winPosPrefab ;
    [SerializeField] private GameObject gemsPrefab ;

    [Header("Containers")]
    [SerializeField] private Transform baseContainer ;
    [SerializeField] private Transform wallContainer ;
    [SerializeField] private Transform stackContainer ;

    [SerializeField] private Transform cornerContainer ;
    [SerializeField] private Transform bridgeContainer ;
    [SerializeField] private Transform winPosContainer ;
    [SerializeField] private Transform gemsContainer ;
   // [SerializeField] private List<GameObject> baseList = new List<GameObject>() ;

    private LevelData currentLevelData ;
    
    void OnAwake()
    {
     //    List<BaseData> baseDataList = new List<BaseData>();
    }
    void Start()
    {
        Debug.Log("Start");
        //OnInit();
    }

    public void OnInit()
    {
        //currentLevelData = LevelLoader.LoadLevel("Assets/_Game/StreamingAssets/Levels/level2.json");
        if(currentLevelData == null) return ;
        else
        {
            GenBase();
            GenWall();
            GenStack();
            GenCorner();
            GenBridge();
            GenWinPos();
            GenGems();
        }
    }
    public Vector3 GetStartPos(){
        return currentLevelData.startPos;
    }
    public void OnEnd()
    {
        foreach(Transform child in baseContainer){
            Destroy(child.gameObject);
        }
        foreach(Transform child in wallContainer){
            Destroy(child.gameObject);
        }
        foreach(Transform child in stackContainer){
            Destroy(child.gameObject);
        }
        foreach(Transform child in cornerContainer){
            Destroy(child.gameObject);
        }
        foreach(Transform child in bridgeContainer){
            Destroy(child.gameObject);
        }
        foreach(Transform child in winPosContainer){
            Destroy(child.gameObject);
        }
        foreach(Transform child in gemsContainer){
            Destroy(child.gameObject);
        }
    }
    public void PlayWinEffect(){
        winPosContainer.GetComponent<ParticleSystem>().Play();
    }
    public void SetLevel(int level){
        currentLevelData = LevelLoader.LoadLevel("Assets/_Game/StreamingAssets/Levels/level" + level + ".json");
    }
}