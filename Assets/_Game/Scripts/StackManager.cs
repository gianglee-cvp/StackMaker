using System.Collections.Generic;
using UnityEngine;


public class StackManager : MonoBehaviour
{

    [SerializeField] CameraFollow cameraFollow; 
    public static StackManager Instance;
    [SerializeField] private Transform stackHolder; // đối tượng cha chứa tất cả stack
    [SerializeField] private Transform playerBody  ; 
    [SerializeField] private float stackHeight = 0.3f ; 
    private List<PoolObject> stackList = new List<PoolObject>();    
    public int StackCount => stackList.Count; 
    public MoveDirection curMoveDirectionHitCorner = MoveDirection.None ; 

    [SerializeField] private Animator playerAnimator ;
    public void OnInit()
    {
            RemoveAllStack();
            playerAnimator.SetInteger(GameConstant.PlayerAnim, GameConstant.AnimIdle); 
    }
    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void RemoveAllStack()
    {
        while(stackList.Count > 0){
            int lastIndex = stackList.Count - 1;
            stackList[lastIndex].gameObject.SetActive(false);
            stackList.RemoveAt(lastIndex);
        }
        playerBody.localPosition = new Vector3(0 , GameConstant.PlayerBodyDefaultY , 0) ; 
        playerBody.localRotation = Quaternion.Euler(0 , GameConstant.PlayerBodyDefaultRotY , 0) ;
    }
    public void AddStack( StackObject stackObject)
    {
        stackObject.transform.SetParent(stackHolder);
        stackObject.transform.localPosition = new Vector3(0 , stackHeight * StackCount + GameConstant.StackBaseYOffset , 0) ;  
        playerBody.localPosition += new Vector3(0 , stackHeight , 0) ;
        
        stackList.Add(stackObject);
        stackObject.stackCollider.enabled = false;
        
        cameraFollow.UpdateCameraMilestone(StackCount);
    }
    public void HitCorner(CornerObject corner)
    {
        PlayerController.Instance.hitCorner = true;
        playerAnimator.SetInteger(GameConstant.PlayerAnim , GameConstant.AnimRun);  

        if(PlayerController.Instance.curMoveDirection == MoveDirection.Up || PlayerController.Instance.curMoveDirection == MoveDirection.Down){
            curMoveDirectionHitCorner = corner.mustMoveHorizontal;
        }
        else if(PlayerController.Instance.curMoveDirection == MoveDirection.Left || PlayerController.Instance.curMoveDirection == MoveDirection.Right){
            curMoveDirectionHitCorner = corner.mustMoveVertical;
        }
    }
    public void HitBridge()
    {
        if(StackCount == 0) return ; 
        stackList[StackCount - 1].gameObject.SetActive(false); 
        stackList.RemoveAt(StackCount - 1);

        playerBody.localPosition -= new Vector3(0 , stackHeight , 0) ;

        if(StackCount == 0)
        {
            if (GameManager.Instance != null) GameManager.Instance.OnDeath(); 
        }

        cameraFollow.UpdateCameraMilestone(StackCount);

    }
    public void OnHitWinPos(WinPosObject winPos)
    {
        PlayerController.Instance.hitWinPos = true;

        if(GameManager.Instance != null) GameManager.Instance.Point = StackCount; 
        winPos.PlayWinEffect(); 
        RemoveAllStack();

        playerBody.localRotation = Quaternion.Euler(0 , GameConstant.WinPosRotationY , 0) ; 
        winPos.OpenTreasure(); // Mở rương kho báu       
        playerAnimator.SetInteger(GameConstant.PlayerAnim , GameConstant.AnimWin); 
    }
    public void OnExitBridge(BridgeObject bridge)
    {
        bridge.SetColor(); 
        bridge.boxCollider.enabled = false; 
    }
    public void HitGem()
    {
        if(GameManager.Instance != null) GameManager.Instance.GemCount++; 
    }
    public void OnExitGem()
    {
        playerAnimator.SetInteger(GameConstant.PlayerAnim , GameConstant.AnimIdle); 
    }
}
