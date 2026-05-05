using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public int row ;
    public int column ;
    public MapGenTag type ; 
    public BridgeDirection bridgeDirection ; // Unity tự hiểu là (0, 1) khi đọc JSON
    public CornerDirection cornerDirection ; // Unity tự hiểu là (0, 1, 2, 3) khi đọc JSON
}
[System.Serializable]
public class LevelData 
{
    public Vector3 startPos ;
    public int rows ; 
    public int columns ;
    public Vector3 baseRotation = new Vector3(-90f, 0f, 0f); // Default rotation cho mọi file JSON lấy LevelData
    public Vector3 wallRotation = new Vector3(-90f, 0f, 0f); // Default rotation cho mọi file JSON lấy LevelData
    public Vector3 stackRotation = new Vector3(-90f, 0f, -180f); // Default rotation cho mọi file JSON lấy LevelData
    public Vector3 winPosRotation = new Vector3(0, 0, 0) ;
    public List<TileData> tileDataList = new List<TileData>() ;
}
[System.Serializable]
public class LevelDataWrapper
{
    public LevelData level ;
}
