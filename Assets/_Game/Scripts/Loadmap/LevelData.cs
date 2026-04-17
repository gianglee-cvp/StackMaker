using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    public int row ; 
    public int column ;
}
[System.Serializable]
public class WallData
{
    public int row ;
    public int column ;
}
[System.Serializable]
public class StackData
{
    public int row ;
    public int column ;
}
[System.Serializable]
public class BridgeData
{
    public int row ;
    public int column ;
    public BridgeDirection direction ; // Unity tự hiểu là (0, 1) khi đọc JSON
}
[System.Serializable]
public class CornerData
{
    public int row ;
    public int column ;
    public CornerDirection direction ; // Unity tự hiểu là (0, 1, 2, 3) khi đọc JSON
}
[System.Serializable]
public class WinPos
{
    public int row ; 
    public int column ; 
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
    public List<BaseData> baseData = new List<BaseData>() ;
    public List<WallData> wallData = new List<WallData>() ;
    public List<StackData> stackData = new List<StackData>() ;
    public List<CornerData> cornerData = new List<CornerData>() ;
    public List<BridgeData> bridgeData = new List<BridgeData>() ;
    public WinPos winPos = new WinPos() ;
}
[System.Serializable]
public class LevelDataWrapper
{
    public LevelData level ;
}
