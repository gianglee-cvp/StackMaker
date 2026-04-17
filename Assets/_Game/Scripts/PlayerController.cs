using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerController Instance;
    public bool isSliding = false;
    public MoveDirection curMoveDirection = MoveDirection.None;
    [SerializeField] private Transform detechWallPoint ; 
    [SerializeField] private LayerMask wallLayer;
    public bool hitCorner = false ;
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
        //    isSliding = false; sẽ sửa trong oncompltete của tween sau
        }
     }
     
    }

    void PlayerMove(MoveDirection moveDirection){
    //    Debug.Log("Player Move: " + moveDirection);
        isSliding = true;
        Vector3 dir = Vector3.zero;
        curMoveDirection = moveDirection;
       // Debug.Log("Current Move Direction: " + curMoveDirection);
        if(curMoveDirection == MoveDirection.None) return ;
        switch (curMoveDirection)
        {
            case MoveDirection.Right:
            dir = Vector3.back;
            break ; 
            case MoveDirection.Left:
            dir = Vector3.forward;
            break;
            case MoveDirection.Up:
            dir = Vector3.right;
            break ; 
            case MoveDirection.Down:
            dir = Vector3.left;
            break;
        }


        int distance = 0 ; 
      //  Debug.DrawRay(detechWallPoint.position, dir * 50f, Color.red , 999f);
        if(Physics.Raycast(detechWallPoint.position,dir, out RaycastHit hit, 50f , wallLayer)){
            distance = Mathf.FloorToInt(Vector3.Distance(hit.point, detechWallPoint.position));
        }
        transform.DOMove(transform.position + dir * distance, 0.09f * distance)
        .SetEase(Ease.InOutQuad)
        .OnUpdate(()=>{
            Debug.Log("Player is Sliding...");
            if(StackManager.Instance.stackCount == 0) transform.DOKill(); // Nếu không còn stack nào thì dừng tween để tránh lỗi khi player vẫn đang di chuyển nhưng đã hết stack
        })
        .OnComplete(()=>{
            if(hitCorner){
                hitCorner = false;
                PlayerMove(StackManager.Instance.curMoveDirectionHitCorner);
            }
            else
            {
                isSliding = false;
                curMoveDirection = MoveDirection.None;
            }
            
        });
        
    }
    void AddBrick()
    {
        
    }
    void RemoveBrick()
    {
        
    }
}
