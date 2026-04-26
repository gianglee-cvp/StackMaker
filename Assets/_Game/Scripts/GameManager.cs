using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Unity.Collections.LowLevel.Unsafe;
using NUnit.Framework;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private MapManager mapManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] public StackManager stackManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ObjectPooler objectPooler;
    private string LEVEL_KEY = "CurrentLevel";
    private string MAX_PLAYER_LEVEL_KEY = "MaxPlayerLevel";

    public int currentLevel = 1;
    public int maxLevel = 5; // level tối đa mà game có
    public int maxPlayerLevel  ; // level tối đa mà player có thể chơi , phải vượt qua để mở khóa thêm 
    private int _gemCount;
    private bool isUIShow; 
    public bool IsUIShow
    {
        get { return isUIShow; }
        set
        {
            isUIShow = value;
        }
    }

    public int GemCount
    {
        get { return _gemCount; }
        set
        {
            _gemCount = value;
        }
    }

    public static Action<string> OnChange; 

    private int _point; 

    public int Point
    {
        get { return _point; }
        set
        {
            _point = value;
            uiManager.UpdateStackCount(_point);
        }
    }
    

    public static GameManager Instance { get; private set; }
    public void OnInit(){
        Point = 0; // Đặt lại điểm số về 0 khi khởi tạo lại level
        GemCount = 0; // Đặt lại số lượng gem về 0 khi khởi tạo lại level
        mapManager.SetLevel(currentLevel);
        mapManager.OnInit();
        player.OnInit(mapManager.GetStartPos());
        stackManager.OnInit();
        cameraFollow.OnInit();
        uiManager.OnInit();
    }
    void Awake()
    {
        IsUIShow = true ; 
        Instance = this;
        objectPooler.OnInit();
        currentLevel = PlayerPrefs.GetInt(LEVEL_KEY, 1);
        maxPlayerLevel = PlayerPrefs.GetInt(MAX_PLAYER_LEVEL_KEY, 1);
        OnInit();
        uiManager.UpdateLevelText(currentLevel);
        uiManager.homePanel.SetActive(true);

        
    }

        // Update is called once per frame
    public void OnWin(){
        uiManager.UpdateStackCount(_point);
        Debug.Log("Player Wins with " + _point + " stacks and " + GemCount + " gems!");
        OnChange?.Invoke("Win");
        uiManager.winPanel.SetActive(true);
        IsUIShow = true; // Hiển thị UI khi chiến thắng
        return ;
    }
    public void OnDeath(){
        uiManager.deathPanel.SetActive(true);
        IsUIShow = true; // Hiển thị UI khi chết
        OnChange?.Invoke("Death");
        return ;
    }
    public void RestartButton(){
        uiManager.winPanel.SetActive(false);
        uiManager.deathPanel.SetActive(false);
        IsUIShow = false; // Ẩn UI khi bắt đầu lại
        mapManager.OnEnd();

        OnChange?.Invoke("Restart"); // winpos nghe để reset 

        OnInit();
    }
    public void NextLevelButton(){
        // không cần gửi sự kiênt nào cả vì khi load level mới thì mọi thứ sẽ được reset lại
        if(currentLevel >= maxLevel){
            currentLevel = 1;
        }
        else currentLevel++;
        PlayerPrefs.SetInt(MAX_PLAYER_LEVEL_KEY, Mathf.Max(maxPlayerLevel, currentLevel)); // Cập nhật maxPlayerLevel nếu currentLevel vượt qua nó
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);
        PlayerPrefs.Save();
        uiManager.winPanel.SetActive(false);
        uiManager.deathPanel.SetActive(false);
        IsUIShow = false;
        mapManager.OnEnd();
        OnInit();
    }
    public void OnPlayButton()
    {
        uiManager.homePanel.SetActive(false);
        IsUIShow = false; // Ẩn UI khi bắt đầu chơi
        if(currentLevel != PlayerPrefs.GetInt(LEVEL_KEY, 1)){
            PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);
            PlayerPrefs.Save();
            mapManager.OnEnd();
            OnInit();
        }
    }
    public void OnChangeLevelButton()
    {
        if(currentLevel >= maxPlayerLevel){
            currentLevel = 1;
        }
        else currentLevel++;
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);
        PlayerPrefs.Save();
        uiManager.UpdateLevelText(currentLevel);

    }
}
