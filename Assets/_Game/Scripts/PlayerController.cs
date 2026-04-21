using UnityEngine;
//using DG.Tweening;
using System.Collections;
//using System.Numerics;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerController Instance;
    public bool isSliding = false;
    public MoveDirection curMoveDirection = MoveDirection.None;
    [SerializeField] private Transform detechWallPoint ; 
    [SerializeField] private LayerMask wallLayer;
    public bool hitCorner = false ;
    public bool hitWinPos = false ;
    private float speed = 10f ;

    private Coroutine moveCoroutine;
    public void OnInit(Vector3 startPos){
        transform.position = startPos;
        isSliding = false;
        curMoveDirection = MoveDirection.None;
        hitCorner = false;
        hitWinPos = false;
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
            PlayerMoveNormal(curMoveDirection);
        //    isSliding = false; sẽ sửa trong oncompltete của tween sau
        }
     }
     
    }

    // void PlayerMove(MoveDirection moveDirection){
    // //    Debug.Log("Player Move: " + moveDirection);
    //     isSliding = true;
    //     Vector3 dir = Vector3.zero;
    //     curMoveDirection = moveDirection;
    //    // Debug.Log("Current Move Direction: " + curMoveDirection);
    //     if(curMoveDirection == MoveDirection.None) return ;
    //     switch (curMoveDirection)
    //     {
    //         case MoveDirection.Right:
    //         dir = Vector3.back;
    //         break ; 
    //         case MoveDirection.Left:
    //         dir = Vector3.forward;
    //         break;
    //         case MoveDirection.Up:
    //         dir = Vector3.right;
    //         break ; 
    //         case MoveDirection.Down:
    //         dir = Vector3.left;
    //         break;
    //     }


    //     int distance = 0 ; 
    //     if(Physics.Raycast(detechWallPoint.position,dir, out RaycastHit hit, 50f , wallLayer)){
    //         distance = Mathf.FloorToInt(Vector3.Distance(hit.point, detechWallPoint.position));
    //     }
    //     transform.DOMove(transform.position + dir * distance, 0.09f * distance)
    //     .SetEase(Ease.InOutQuad)
    //     .OnUpdate(()=>{
    //         Debug.Log("Player is Sliding...");
    //          if(StackManager.Instance.stackCount == 0 && !hitWinPos) transform.DOKill(); // Nếu không còn stack nào thì dừng tween để tránh lỗi khi player vẫn đang di chuyển nhưng đã hết stack
    //     })
    //     .OnComplete(()=>{
    //         if(hitCorner){
    //             hitCorner = false;
    //             PlayerMove(StackManager.Instance.curMoveDirectionHitCorner);
    //         }
    //         else if(hitWinPos)
    //         {
    //             Debug.Log("Player reached Win Position!");
    //             GameManager.Instance.OnWin(); // Reset game when player reaches win position
    //         }
    //         else
    //         {
    //             isSliding = false;
    //             curMoveDirection = MoveDirection.None;
    //         }
            
    //     });
        
    // }
    void PlayerMoveNormal(MoveDirection moveDirection){
        isSliding = true;
        Vector3 dir = Vector3.zero;
        curMoveDirection = moveDirection;
        if(curMoveDirection == MoveDirection.None)
        {
            isSliding = false;
            return ;
        }
        switch (moveDirection)
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
        if(Physics.Raycast(detechWallPoint.position,dir, out RaycastHit hit, 50f , wallLayer)){
            distance = Mathf.FloorToInt(Vector3.Distance(hit.point, detechWallPoint.position));
        }
        Vector3  targetPos = transform.position + dir * distance;
        targetPos = SnapToGrid(targetPos); // Làm tròn vị trí đến gần nhất trên lưới để tránh lỗi do số lẻ khi di chuyển
        if(moveCoroutine != null){
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToPosition(targetPos));
        

    }

    private IEnumerator MoveToPosition(Vector3 targetPos){
        while(Vector3.Distance(transform.position, targetPos) > 0.01f){
            if(StackManager.Instance.stackCount == 0 && !hitWinPos) yield break; // Nếu không còn stack nào thì dừng coroutine để tránh lỗi khi player vẫn đang di chuyển nhưng đã hết stack
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );
            yield return null;
        }
        transform.position = targetPos; // Đảm bảo vị trí cuối cùng chính xác
        if(hitCorner){
            hitCorner = false;
            PlayerMoveNormal(StackManager.Instance.curMoveDirectionHitCorner);
        }
        else if(hitWinPos)
        {
            curMoveDirection = MoveDirection.None; // Reset hướng để Update không gọi lại
            GameManager.Instance.OnWin(); // Reset game when player reaches win position
        }
        else
        {
            // cần else ở ddaay bởi vì nếu player chạm vào góc mà sẻ direction về none thì sẽ khoogn thể so sánh trong stackmanager được
            curMoveDirection = MoveDirection.None;
        }
        isSliding = false;
        yield break;

    }
    void AddBrick()
    {
        
    }
    void RemoveBrick()
    {
        
    }
    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x);
        float y = Mathf.Round(position.y);
        float z = Mathf.Round(position.z);
        return new Vector3(x, y, z);
    }
}
