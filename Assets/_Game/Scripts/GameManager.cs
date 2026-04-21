using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private MapManager mapManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private StackManager stackManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ObjectPooler objectPooler;
    public int currentLevel = 1;
    public int maxLevel = 2;
    private int _gemCount;

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
        Instance = this;
        objectPooler.OnInit();
        currentLevel = 2;
        OnInit();
    }

        // Update is called once per frame
    void Update()
    {
        
    }
    public void OnWin(){
        uiManager.UpdateStackCount(_point);
        Debug.Log("Player Wins with " + _point + " stacks and " + GemCount + " gems!");
        OnChange?.Invoke("Win");
        uiManager.winPanel.SetActive(true);
        return ;
    }
    public void OnDeath(){
        uiManager.deathPanel.SetActive(true);
        OnChange?.Invoke("Death");
        return ;
    }
    public void RestartButton(){
        uiManager.winPanel.SetActive(false);
        uiManager.deathPanel.SetActive(false);
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
        uiManager.winPanel.SetActive(false);
        uiManager.deathPanel.SetActive(false);
        mapManager.OnEnd();
        OnInit();
    }
}
