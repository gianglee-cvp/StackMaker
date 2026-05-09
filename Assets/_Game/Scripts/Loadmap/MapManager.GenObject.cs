using UnityEngine;
using System.Collections.Generic;

public partial class MapManager : MonoBehaviour
{

    Dictionary<MapGenTag, TileConfig> config;
    public void SetMapConfig()
    {
        config = new Dictionary<MapGenTag, TileConfig>()
        {
            { MapGenTag.Base, new TileConfig(basePoolPrefab, baseContainer, 0f,new  Vector3(-90, 0 , 0)) },
            { MapGenTag.Wall, new TileConfig(wallPrefab, wallContainer, 2.865f, new Vector3(-90, 0, 0)) },
            { MapGenTag.Stack, new TileConfig(stackPrefab, stackContainer, 2.5f, new Vector3(-90, 0, -180)) },
            { MapGenTag.Corner, new TileConfig(cornerPrefab, cornerContainer, 2.5f, Vector3.zero) },
            { MapGenTag.Bridge, new TileConfig(bridgePoolPrefab, bridgeContainer, 2.5f, Vector3.zero) },
            { MapGenTag.WinPos, new TileConfig(winPosPrefab, winPosContainer, 0f, new Vector3(0, 90, 0)) },
            { MapGenTag.Gems, new TileConfig(gemsPrefab, gemsContainer, 0f, Vector3.zero) }
        };
    }
    public void GenTile(TileData tileData)
    {
        if (!config.ContainsKey(tileData.type))
        {
            return; 
        }
        TileConfig tileConfig = config[tileData.type];
        Vector3 rotation = tileConfig.rotationOffset;
        if(tileData.type == MapGenTag.Corner)
        {
            rotation = new Vector3(0, (int)tileData.cornerDirection * GameConstant.CornerRotationStep, 0);
        }
        else if (tileData.type == MapGenTag.Bridge)
        {
            rotation = new Vector3(-90, 0, (int)tileData.bridgeDirection * GameConstant.CornerRotationStep);
        }
        PoolObject tileObject = ObjectPooler.Instance.SpawnFromPool(
            tileConfig.prefab,
            tileData.type,
            new Vector3(tileData.row, tileConfig.yOffset, tileData.column),
            Quaternion.Euler(rotation) , tileConfig.parent
        );
        if(!listObjectsActive.ContainsKey(tileData.type))
        {
            listObjectsActive.Add(tileData.type , new Queue<PoolObject>()) ;
        }
        listObjectsActive[tileData.type].Enqueue(tileObject);

    }
    
}
