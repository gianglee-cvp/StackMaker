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
             //   Instantiate(basePrefab, new Vector3(baseData.row, 0, baseData.column), Quaternion.Euler(currentLevelData.baseRotation) , baseContainer);
                ObjectPooler.Instance.SpawnFromPool( basePrefab , MapGenTag.Base , new Vector3(baseData.row, 0, baseData.column), Quaternion.Euler(currentLevelData.baseRotation) , baseContainer);
            }
        }
    }
    private void GenWall()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach( var wallData in currentLevelData.wallData)
            {
             //   Instantiate(wallPrefab, new Vector3(wallData.row, 2.865f, wallData.column), Quaternion.Euler(currentLevelData.wallRotation) , wallContainer);
                ObjectPooler.Instance.SpawnFromPool( wallPrefab , MapGenTag.Wall , new Vector3(wallData.row, 2.865f, wallData.column), Quaternion.Euler(currentLevelData.wallRotation) , wallContainer);
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
              //  Instantiate(stackPrefab, new Vector3(stackData.row, 2.5f, stackData.column), Quaternion.Euler(currentLevelData.stackRotation) , stackContainer);
                ObjectPooler.Instance.SpawnFromPool( stackPrefab , MapGenTag.Stack , new Vector3(stackData.row, 2.5f, stackData.column), Quaternion.Euler(currentLevelData.stackRotation) , stackContainer);
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
                float angle = (int)cornerData.direction * 90f;
              //  Instantiate(cornerPrefab, new Vector3(cornerData.row, 2.5f, cornerData.column), Quaternion.Euler(0, angle, 0), cornerContainer);
                ObjectPooler.Instance.SpawnFromPool( cornerPrefab , MapGenTag.Corner , new Vector3(cornerData.row, 2.5f, cornerData.column), Quaternion.Euler(0, angle, 0), cornerContainer);
            }
        }
    }
    private void GenBridge()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach( var bridgeData in currentLevelData.bridgeData)
            {
                float angle = (int)bridgeData.direction * 90f; // Horizontal -> 0, Vertical -> 90
               ObjectPooler.Instance.SpawnFromPool( bridgePrefab , MapGenTag.Bridge , new Vector3(bridgeData.row, 2.5f, bridgeData.column), Quaternion.Euler(-90, 0, angle), bridgeContainer);
            }
        }
    }
    private void GenWinPos()
    {
        if(currentLevelData == null || currentLevelData.winPos.row == -999) return ;
        else
        {
           // Instantiate(winPosPrefab, new Vector3(currentLevelData.winPos.row, 0, currentLevelData.winPos.column), Quaternion.Euler(currentLevelData.winPosRotation) , winPosContainer);
            ObjectPooler.Instance.SpawnFromPool( winPosPrefab , MapGenTag.WinPos , new Vector3(currentLevelData.winPos.row, 0, currentLevelData.winPos.column), Quaternion.Euler(currentLevelData.winPosRotation) , winPosContainer);
        }
    }
    private void GenGems()
    {
        if(currentLevelData == null) return ;
        else
        {
            foreach( var gemsData in currentLevelData.gemsData)
            {
                //Instantiate(gemsPrefab, new Vector3(gemsData.row, 3.5f, gemsData.column), Quaternion.Euler(currentLevelData.stackRotation) , gemsContainer);
               ObjectPooler.Instance.SpawnFromPool( gemsPrefab , MapGenTag.Gems , new Vector3(gemsData.row, 3.5f, gemsData.column), Quaternion.Euler(currentLevelData.stackRotation) , gemsContainer);
            }
        }
    }
}
