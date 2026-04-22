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
        while(baseContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Base , baseContainer.GetChild(0).gameObject) ;
        }
        while(wallContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Wall , wallContainer.GetChild(0).gameObject) ;
        }
        while(stackContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Stack , stackContainer.GetChild(0).gameObject) ;
        }
        while(cornerContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Corner , cornerContainer.GetChild(0).gameObject) ;
        }
        while(bridgeContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Bridge , bridgeContainer.GetChild(0).gameObject) ;
        }
        while(winPosContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.WinPos , winPosContainer.GetChild(0).gameObject) ;
        }
        while(gemsContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Gems , gemsContainer.GetChild(0).gameObject) ;
        }
    }

    public void SetLevel(int level){
        currentLevelData = LevelLoader.LoadLevel("Assets/_Game/StreamingAssets/Levels/level" + level + ".json");
    }
}