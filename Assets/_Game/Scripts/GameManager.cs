using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private MapManager mapManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private StackManager stackManager;
    
    public static GameManager Instance { get; private set; }
    public void OnInit(){
        mapManager.OnInit();
        player.OnInit(mapManager.GetStartPos());
        stackManager.Oninit();
        cameraFollow.OnInit();
    }
    void Awake()
    {
        Instance = this;
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnWin(){
        mapManager.OnEnd();
        OnInit();
    }
}
