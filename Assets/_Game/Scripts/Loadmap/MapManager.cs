using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;

public partial class MapManager : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private PoolObject basePoolPrefab ;
    [SerializeField] private PoolObject wallPrefab ;
    [SerializeField] private PoolObject stackPrefab ;
    [SerializeField] private PoolObject cornerPrefab ;
    [SerializeField] private PoolObject bridgePoolPrefab ;
    [SerializeField] private PoolObject winPosPrefab ;
    [SerializeField] private GameObject gemsPrefab ;



    [Header("Containers")]
    [SerializeField] private Transform baseContainer ;
    [SerializeField] private Transform wallContainer ;
    [SerializeField] private Transform stackContainer ;

    [SerializeField] private Transform cornerContainer ;
    [SerializeField] private Transform bridgeContainer ;
    [SerializeField] private Transform winPosContainer ;
    [SerializeField] private Transform gemsContainer ;
    private List<PoolObject> listBaseObjectsActive = new List<PoolObject>() ;
    private List<PoolObject> listWallObjectsActive = new List<PoolObject>() ;
    private List<PoolObject> listStackObjectsActive = new List<PoolObject>() ;
    private List<PoolObject> listCornerObjectsActive = new List<PoolObject>() ;
    private List<PoolObject> listBridgeObjectsActive = new List<PoolObject>() ;
    private List<PoolObject> listWinPosObjectsActive = new List<PoolObject>() ;

    private LevelData currentLevelData ;
    int lastIndex = 0 ; 

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
        while(listBaseObjectsActive.Count > 0){
            lastIndex = listBaseObjectsActive.Count - 1;
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Base , listBaseObjectsActive[lastIndex]) ;
            listBaseObjectsActive.RemoveAt(lastIndex) ;
        }
        while(listWallObjectsActive.Count > 0){
            lastIndex = listWallObjectsActive.Count - 1;
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Wall , listWallObjectsActive[lastIndex]) ;
            listWallObjectsActive.RemoveAt(lastIndex) ;
        }
        while(listStackObjectsActive.Count > 0){
            lastIndex = listStackObjectsActive.Count - 1;
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Stack , listStackObjectsActive[lastIndex]) ;
            listStackObjectsActive.RemoveAt(lastIndex) ;
        }
        while(listCornerObjectsActive.Count > 0){
            lastIndex = listCornerObjectsActive.Count - 1;
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Corner , listCornerObjectsActive[lastIndex]) ;
            listCornerObjectsActive.RemoveAt(lastIndex) ;
        }
        while(listBridgeObjectsActive.Count > 0){
            lastIndex = listBridgeObjectsActive.Count - 1;
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Bridge , listBridgeObjectsActive[lastIndex]) ;
            listBridgeObjectsActive.RemoveAt(lastIndex) ;
        }
        while(listWinPosObjectsActive.Count > 0){
            lastIndex = listWinPosObjectsActive.Count - 1;
            ObjectPooler.Instance.ReturnToPool(MapGenTag.WinPos , listWinPosObjectsActive[lastIndex]) ;
            listWinPosObjectsActive.RemoveAt(lastIndex) ;
        }
        while(gemsContainer.childCount > 0){
            ObjectPooler.Instance.ReturnToPool(MapGenTag.Gems , gemsContainer.GetChild(0).gameObject) ;
        }
    }

    public void SetLevel(int level){
        currentLevelData = LevelLoader.LoadLevel("Assets/_Game/StreamingAssets/Levels/level" + level + ".json");
    }
}