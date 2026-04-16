using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private MapManager mapManager;
    [SerializeField] private PlayerController player;

    void OnInit(){
        mapManager.OnInit();
        player.OnInit(mapManager.GetStartPos());
    }
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
