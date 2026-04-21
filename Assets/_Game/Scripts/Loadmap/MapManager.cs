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
            //Destroy(child.gameObject);
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Base , child.gameObject) ;
        }
        foreach(Transform child in wallContainer){
            //Destroy(child.gameObject);
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Wall , child.gameObject) ;
        }
        foreach(Transform child in stackContainer){
            //Destroy(child.gameObject);
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Stack , child.gameObject) ;
        }
        foreach(Transform child in cornerContainer){
            //Destroy(child.gameObject);
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Corner , child.gameObject) ;
        }
        foreach(Transform child in bridgeContainer){
            //Destroy(child.gameObject);
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Bridge , child.gameObject) ;
        }
        foreach(Transform child in winPosContainer){
           // Destroy(child.gameObject);
            ObjectPooler.Instance.ReturnToPool(MapGenTag.WinPos , child.gameObject) ;
        }
        foreach(Transform child in gemsContainer){
           // Destroy(child.gameObject);
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Gems , child.gameObject) ;
        }
    }

    public void SetLevel(int level){
        currentLevelData = LevelLoader.LoadLevel("Assets/_Game/StreamingAssets/Levels/level" + level + ".json");
    }
}