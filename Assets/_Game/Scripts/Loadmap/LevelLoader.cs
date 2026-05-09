using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    public static LevelData LoadLevel(int levelNumber)
    {
        string json = Resources.Load<TextAsset>($"Levels/Level{levelNumber}").text;
        LevelDataWrapper wrapper = JsonUtility.FromJson<LevelDataWrapper>(json);
        LevelData level = wrapper.level;
        return level ; 
    }
}