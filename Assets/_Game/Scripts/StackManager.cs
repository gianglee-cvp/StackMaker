using System.Collections.Generic;
using UnityEngine;


public class StackManager : MonoBehaviour
{

    [SerializeField] CameraFollow cameraFollow; 
    public static StackManager Instance;
    public Transform stackHolder; // đối tượng cha chứa tất cả stack
    public Transform playerBody  ; 
    public float stackHeight = 0.3f ; 
    public List<PoolObject> stackList = new List<PoolObject>();    
    public int stackCount ; 
    public MoveDirection curMoveDirectionHitCorner = MoveDirection.None ; 

    [SerializeField] private Animator playerAnimator ;
    public void OnInit()
    {
            RemoveAllStack();
            playerAnimator.SetInteger(GameConstant.PlayerAnim, 0); 
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
        stackList.Clear();
        stackCount = 0 ;
        playerBody.localPosition = new Vector3(0 , -0.3f , 0) ; 
        playerBody.localRotation = Quaternion.Euler(0 , 90 , 0) ;
    }
    public void AddStack( StackObject stackObject)
    {
        stackList.Add(stackObject);

        stackObject.trans.SetParent(stackHolder);
        stackObject.trans.localPosition = new Vector3(0 , stackHeight * stackCount - 0.5f , 0) ;  
        playerBody.localPosition += new Vector3(0 , stackHeight , 0) ;
        stackCount++;
        stackObject.stackCollider.enabled = false;
        
        cameraFollow.UpdateCameraMilestone(stackCount);
    }
    public void HitCorner(CornerObject corner)
    {
        PlayerController.Instance.hitCorner = true;
        playerAnimator.SetInteger(GameConstant.PlayerAnim , 1);  

        if(PlayerController.Instance.curMoveDirection == MoveDirection.Up || PlayerController.Instance.curMoveDirection == MoveDirection.Down){
            curMoveDirectionHitCorner = corner.mustMoveHorizontal;
        }
        else if(PlayerController.Instance.curMoveDirection == MoveDirection.Left || PlayerController.Instance.curMoveDirection == MoveDirection.Right){
            curMoveDirectionHitCorner = corner.mustMoveVertical;
        }
    }
    public void HitBridge(Collider other)
    {
        if(stackCount == 0) return ; 
        stackList[stackCount - 1].gameObject.SetActive(false); 
        stackList.RemoveAt(stackCount - 1);
        stackCount--;

        playerBody.localPosition -= new Vector3(0 , stackHeight , 0) ;

        if(stackCount == 0)
        {
            GameManager.Instance.OnDeath(); 
        }

        cameraFollow.UpdateCameraMilestone(stackCount);

    }
    public void OnHitWinPos(WinPosObject winPos)
    {
        PlayerController.Instance.hitWinPos = true;

        GameManager.Instance.Point = stackCount; 
        winPos.PlayWinEffect(); 
        RemoveAllStack();

        playerBody.localRotation = Quaternion.Euler(0 , -90f , 0) ; 
        winPos.OpenTreasure(); // Mở rương kho báu       
        playerAnimator.SetInteger(GameConstant.PlayerAnim , 2); 
    }
    public void OnExitBridge(BridgeObject bridge)
    {
        bridge.SetColor(); 
        bridge.boxCollider.enabled = false; 
    }
    public void HitGem()
    {
        GameManager.Instance.GemCount++; 
    }
    public void OnExitGem()
    {
        playerAnimator.SetInteger(GameConstant.PlayerAnim , 0); 
    }
}
