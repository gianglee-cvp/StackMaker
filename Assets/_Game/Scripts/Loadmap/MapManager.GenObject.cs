using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;

public partial class MapManager : MonoBehaviour
{
    private void GenBase()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach( var baseData in currentLevelData.baseData)
            {
               // Debug.Log("BaseData: Row: " + baseData.row + " Column: " + baseData.column);
                Instantiate(basePrefab, new Vector3(baseData.row, 0, baseData.column), Quaternion.Euler(currentLevelData.baseRotation) , baseContainer);
            }
        }
    }
    private void GenWall()
    {
        if(currentLevelData == null) return ;
        else
        {
        //    Debug.Log("GenWall");
        //    Debug.Log("WallData Count: " + currentLevelData.wallData.Count);
            foreach( var wallData in currentLevelData.wallData)
            {
            //    Debug.Log("WallData: Row: " + wallData.row + " Column: " + wallData.column);
                Instantiate(wallPrefab, new Vector3(wallData.row, 2.865f, wallData.column), Quaternion.Euler(currentLevelData.wallRotation) , wallContainer);
            }
        }
    }
    private void GenStack()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach( var stackData in currentLevelData.stackData)
            {
              //  Debug.Log("StackData: Row: " + stackData.row + " Column: " + stackData.column);
                Instantiate(stackPrefab, new Vector3(stackData.row, 2.5f, stackData.column), Quaternion.Euler(currentLevelData.stackRotation) , stackContainer);
            }
        }
    }
    private void GenCorner()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach( var cornerData in currentLevelData.cornerData)
            {
             //   Debug.Log("CornerData: Row: " + cornerData.row + " Column: " + cornerData.column);
                float angle = (int)cornerData.direction * 90f;
                Instantiate(cornerPrefab, new Vector3(cornerData.row, 2.5f, cornerData.column), Quaternion.Euler(0, angle, 0), cornerContainer);
            }
        }
    }
    private void GenBridge()
    {
        if(currentLevelData == null) return ;
        else
        {
         //   Debug.Log("GenBridge");
            foreach( var bridgeData in currentLevelData.bridgeData)
            {
              //  Debug.Log("BridgeData: Row: " + bridgeData.row + " Column: " + bridgeData.column);
                float angle = (int)bridgeData.direction * 90f; // Horizontal -> 0, Vertical -> 90
                Instantiate(bridgePrefab, new Vector3(bridgeData.row, 2.5f, bridgeData.column), Quaternion.Euler(-90, 0, angle), bridgeContainer);
            }
        }
    }
    private void GenWinPos()
    {
        if(currentLevelData == null || currentLevelData.winPos.row == -999) return ;
        else
        {
          //  Debug.Log("GenWinPos");
            Instantiate(winPosPrefab, new Vector3(currentLevelData.winPos.row, 0, currentLevelData.winPos.column), Quaternion.Euler(currentLevelData.winPosRotation) , winPosContainer);
        }
    }
    private void GenGems()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach( var gemsData in currentLevelData.gemsData)
            {
                //  Debug.Log("StackData: Row: " + stackData.row + " Column: " + stackData.column);
                Instantiate(gemsPrefab, new Vector3(gemsData.row, 3.5f, gemsData.column), Quaternion.Euler(currentLevelData.stackRotation) , gemsContainer);
            }
        }
    }
}
