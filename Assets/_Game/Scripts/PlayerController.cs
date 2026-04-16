using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static PlayerController Instance;
    public bool isSliding = false;
    public MoveDirection curMoveDirection = MoveDirection.None;

    public void OnInit(Vector3 startPos){
        transform.position = startPos;
    }
    void OnEnable()
    {
        InputManager.OnSwipe += HandleSwipe;
    }
    void OnDisable()
    {
        InputManager.OnSwipe -= HandleSwipe;
    }
    void HandleSwipe(MoveDirection moveDirection){
      //  Debug.Log("Swipe: " + moveDirection);
        if(!isSliding){
            curMoveDirection = moveDirection;
        }
    }
    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
     if(isSliding) return;
     else{
        if(curMoveDirection == MoveDirection.None){
            return ;
        }
        else{
            PlayerMove(curMoveDirection);
            isSliding = false;
        }
     }
     
    }

    void PlayerMove(MoveDirection moveDirection){
         Debug.Log("Player Move: " + moveDirection);
        curMoveDirection = MoveDirection.None;
        isSliding = true;
    }
}
