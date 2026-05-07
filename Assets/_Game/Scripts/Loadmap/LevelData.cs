using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public int row ;
    public int column ;
    public MapGenTag type ; 
    public BridgeDirection bridgeDirection ; 
    public CornerDirection cornerDirection ; 
}
[System.Serializable]
public class LevelData 
{
    public Vector3 startPos ;
    public int rows ; 
    public int columns ;
    public Vector3 baseRotation = new Vector3(-90f, 0f, 0f); 
    public Vector3 wallRotation = new Vector3(-90f, 0f, 0f); 
    public Vector3 stackRotation = new Vector3(-90f, 0f, -180f);
    public Vector3 winPosRotation = new Vector3(0, 0, 0) ;
    public List<TileData> tileDataList = new List<TileData>() ;
}
[System.Serializable]
public class LevelDataWrapper
{
    public LevelData level ;
}
