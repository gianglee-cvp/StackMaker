using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    public static LevelData LoadLevel(string jsonFileName)
    {
        string json = File.ReadAllText(jsonFileName);
        LevelDataWrapper wrapper = JsonUtility.FromJson<LevelDataWrapper>(json);
        LevelData level = wrapper.level;
        return level ; 
    }
}