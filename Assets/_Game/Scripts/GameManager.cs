using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private MapManager mapManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private StackManager stackManager;

    void OnInit(){
        mapManager.OnInit();
        player.OnInit(mapManager.GetStartPos());
        stackManager.Oninit();
        cameraFollow.OnInit();
    }
    void Awake()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
