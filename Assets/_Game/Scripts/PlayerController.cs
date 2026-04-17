using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static PlayerController Instance;
    public bool isSliding = false;
    private MoveDirection curMoveDirection = MoveDirection.None;
    [SerializeField] private Transform detechWallPoint ; 
    [SerializeField] private LayerMask wallLayer;
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
        Debug.Log("Player Move: " + moveDirection);
        isSliding = true;
        Vector3 dir = Vector3.zero;
        Debug.Log("Current Move Direction: " + curMoveDirection);
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
        //Debug.Log("Player Move Direction: " + dir);
        //Debug.DrawRay(detechWallPoint.position, dir * 50f, Color.red , 999f);


        int distance = 0 ; 

        if(Physics.Raycast(detechWallPoint.position,dir, out RaycastHit hit, 50f , wallLayer)){
            distance = Mathf.FloorToInt(Vector3.Distance(hit.point, detechWallPoint.position));
        }
        transform.DOMove(transform.position + dir * distance, 0.1f * distance)
        .SetEase(Ease.InOutQuad)
        .OnUpdate(()=>{
            Debug.Log("Player is Sliding...");
        })
        .OnComplete(()=>{
            Debug.Log("Player Slide Complete");
            isSliding = false;
        });
        curMoveDirection = MoveDirection.None;
    }
    void AddBrick()
    {
        
    }
    void RemoveBrick()
    {
        
    }
}
