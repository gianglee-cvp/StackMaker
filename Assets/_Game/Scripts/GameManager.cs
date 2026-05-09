using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] public StackManager stackManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ObjectPooler objectPooler;
    private DataManager dataManager = new DataManager();

    private int currentLevel = 1;
    [SerializeField] private int maxLevel = 5; // level tối đa mà game có
    private int maxPlayerLevel  ; // level tối đa mà player có thể chơi , phải vượt qua để mở khóa thêm 
    public bool IsUIShow { get; set; }
    public int GemCount { get; set; }


    private int _point; 
    public int Point
    {
        get => _point;
        set
        {
            _point = value;
            uiManager.UpdateStackCount(_point);
        }
    }
    

    public static GameManager Instance { get; private set; }
    public void OnInit(){
        Point = 0; 
        GemCount = 0; 
        Time.timeScale = 1f; 
        stackManager.OnInit(); 

        mapManager.SetLevel(currentLevel);
        mapManager.OnInit();
        player.OnInit(mapManager.GetStartPos());
        cameraFollow.OnInit();
        uiManager.OnInit();
    }
    private void Awake()
    { 
        Instance = this;
        objectPooler.OnInit();
        currentLevel = dataManager.getCurrentLevel();
        maxPlayerLevel = dataManager.getMaxPlayerLevel();
        uiManager.OnAwake();
        mapManager.SetMapConfig();
        OnInit();
        uiManager.UpdateLevelText(currentLevel);
        uiManager.OnChangeUI(GameState.Home);

    }
    public void OnWin(){
        uiManager.UpdateStackCount(_point);
        Debug.Log("Player Wins with " + _point + " stacks and " + GemCount + " gems!");
        uiManager.OnChangeUI(GameState.Win);
    }
    public void OnDeath(){
        uiManager.OnChangeUI(GameState.Lose);
    }
    public void RestartButton(){
        uiManager.OnChangeUI(GameState.Playing);
        mapManager.OnEnd();
        OnInit();
    }
    public void NextLevelButton(){
        if(currentLevel >= maxLevel){
            currentLevel = 1;
        }
        else currentLevel++;
        dataManager.SaveNextLevel(currentLevel);
        uiManager.OnChangeUI(GameState.Playing);
        mapManager.OnEnd();
        OnInit();
    }
    public void OnPlayButton()
    {
        uiManager.OnChangeUI(GameState.Playing);
        dataManager.SaveLevel(currentLevel);
        mapManager.OnEnd();
        OnInit();
    }
    public void OnChangeLevelButton()
    {
        if(currentLevel >= maxPlayerLevel){
            currentLevel = 1;
        }
        else currentLevel++;
        if(currentLevel != dataManager.getCurrentLevel()){
            mapManager.OnEnd();
            OnInit();
        }
        dataManager.SaveLevel(currentLevel);
        uiManager.UpdateLevelText(currentLevel);

    }
    public void OnChangeLevelRightLeftButton(int i){
        currentLevel += i;
        if(currentLevel > maxPlayerLevel){
            currentLevel = 1;
        }
        else if(currentLevel <= 0){
            currentLevel = maxPlayerLevel;
        }
        uiManager.UpdateLevelText(currentLevel);
    }
    public void OnPauseButton()
    {
        uiManager.OnChangeUI(GameState.Pause);
        Time.timeScale = 0f; 
    }
    public void OnResumeButton()
    {
        uiManager.OnChangeUI(GameState.Playing);
        Time.timeScale = 1f; 
    }
    public void OnHomeButton()
    {

        uiManager.OnChangeUI(GameState.Home);
        mapManager.OnEnd();
    }   
}
