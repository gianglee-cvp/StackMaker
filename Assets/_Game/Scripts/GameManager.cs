using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private MapManager mapManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraFollow cameraFollow;

    void OnInit(){
        mapManager.OnInit();
        player.OnInit(mapManager.GetStartPos());
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
