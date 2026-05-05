using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class MapManager : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private PoolObject basePoolPrefab ;
    [SerializeField] private PoolObject wallPrefab ;
    [SerializeField] private PoolObject stackPrefab ;
    [SerializeField] private PoolObject cornerPrefab ;
    [SerializeField] private PoolObject bridgePoolPrefab ;
    [SerializeField] private PoolObject winPosPrefab ;
    [SerializeField] private PoolObject gemsPrefab ;



    [Header("Containers")]
    [SerializeField] private Transform baseContainer ;
    [SerializeField] private Transform wallContainer ;
    [SerializeField] private Transform stackContainer ;

    [SerializeField] private Transform cornerContainer ;
    [SerializeField] private Transform bridgeContainer ;
    [SerializeField] private Transform winPosContainer ;
    [SerializeField] private Transform gemsContainer ;

    private Dictionary<MapGenTag, Queue<PoolObject>> listObjectsActive = new Dictionary<MapGenTag, Queue<PoolObject>>() ;

    private LevelData currentLevelData ;

    public void OnInit()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach(TileData tileData in currentLevelData.tileDataList)
            {
                GenTile(tileData) ;
            }
        }
    }
    public Vector3 GetStartPos(){
        return currentLevelData.startPos;
    }
    public void OnEnd()
    {
        foreach( var pair in listObjectsActive)
        {
            MapGenTag tag = pair.Key ;
            Queue<PoolObject> queue = pair.Value ;
            // Gán tham chiếu chứ không phải copy0

            while(queue.Count > 0)
            {
                var obj = queue.Dequeue() ;
                ObjectPooler.Instance.ReturnToPool(tag , obj) ;
            }
        }
        listObjectsActive.Clear() ;       
    }

    public void SetLevel(int level){
        currentLevelData = LevelLoader.LoadLevel("Assets/_Game/StreamingAssets/Levels/level" + level + ".json");
    }
}