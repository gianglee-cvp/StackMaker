using System;
using UnityEngine;
public class DataManager
{
    public const string LEVEL_KEY = "CurrentLevel";
    public const string MAX_PLAYER_LEVEL_KEY = "MaxPlayerLevel";

    public void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL_KEY, level);
        PlayerPrefs.Save();
    }
    public int getCurrentLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY, 1);
    }

    public int getMaxPlayerLevel()
    {
        return PlayerPrefs.GetInt(MAX_PLAYER_LEVEL_KEY, 1);
    }
    public void SaveNextLevel(int currentLevel)
    {
        PlayerPrefs.SetInt(MAX_PLAYER_LEVEL_KEY , Math.Max(currentLevel, getMaxPlayerLevel()));
        SaveLevel(currentLevel);

    }

}