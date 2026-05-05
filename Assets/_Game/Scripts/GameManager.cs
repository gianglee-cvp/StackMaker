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
        Time.timeScale = 1f; // Đảm bảo thời gian được đặt lại về bình thường khi khởi tạo lại level
        stackManager.OnInit(); // Gọi trước mapManager.OnInit để clear stack thừa, không vô tình tắt nhầm stack mới

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
        OnChange?.Invoke("Win");
        uiManager.OnChangeUI(GameState.Win);
    }
    public void OnDeath(){
        uiManager.OnChangeUI(GameState.Lose);
        OnChange?.Invoke("Death");
    }
    public void RestartButton(){
        uiManager.OnChangeUI(GameState.Playing);
        mapManager.OnEnd();
        OnChange?.Invoke("Restart"); 
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
        Time.timeScale = 0f; // Tạm dừng thời gian trong game
    }
    public void OnResumeButton()
    {
        uiManager.OnChangeUI(GameState.Playing);
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
    }
    public void OnHomeButton()
    {

        uiManager.OnChangeUI(GameState.Home);
        mapManager.OnEnd();
    }   
}
