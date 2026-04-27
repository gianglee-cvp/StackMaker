using System.Collections.Generic;
using UnityEngine;


public class StackManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] CameraFollow cameraFollow; // Kéo Camera vào đây để gọi hàm cập nhật mốc khi số lượng gạch thay đổi
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
            playerAnimator.SetInteger(GameConstant.PlayerAnim, 0); // Đặt lại animation về trạng thái ban đầu

             // Cập nhật mốc Camera khi số lượng gạch thay đổi
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

    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            GameManager.Instance.GemCount++; // Cập nhật số lượng gem khi thu thập được
            other.gameObject.SetActive(false); // Vô hiệu hóa viên gem đã thu thập
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Corner"))
        {
            playerAnimator.SetInteger(GameConstant.PlayerAnim , 0);
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
        playerBody.localPosition = new Vector3(0 , -0.3f , 0) ; // Đặt lại vị trí của player body về vị trí ban đầu
        playerBody.localRotation = Quaternion.Euler(0 , 90 , 0) ;
    }
    public void AddStack( StackObject stackObject)
    {
        stackList.Add(stackObject);

        stackObject.trans.SetParent(stackHolder);
        stackObject.trans.localPosition = new Vector3(0 , stackHeight * stackCount - 0.5f , 0) ;  
        playerBody.localPosition += new Vector3(0 , stackHeight , 0) ;
        stackCount++;
        stackObject.stackCollider.enabled = false; // Vô hiệu hóa collider của gạch đã thu thập
        
        // Cập nhật mốc Camera khi số lượng gạch thay đổi
        if (cameraFollow != null)
        {
            cameraFollow.UpdateCameraMilestone(stackCount);
        }
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
        if(stackCount == 0) return ; // Nếu không còn stack nào thì không làm gì cả

       // ObjectPooler.Instance.ReturnToPool(MapGenTag.Stack , stackList[stackCount- 1]);
        stackList[stackCount - 1].gameObject.SetActive(false); 
        stackList.RemoveAt(stackCount - 1);
        stackCount--;

        playerBody.localPosition -= new Vector3(0 , stackHeight , 0) ;

        if(stackCount == 0)
        {
            GameManager.Instance.OnDeath(); 
        }


            // Cập nhật mốc Camera khi số lượng gạch thay đổi
        if (cameraFollow != null)
        {
            cameraFollow.UpdateCameraMilestone(stackCount);
        }

    }
    public void OnHitWinPos(WinPosObject winPos)
    {
        PlayerController.Instance.hitWinPos = true;

        GameManager.Instance.Point = stackCount; // Cập nhật điểm khi đến vị trí chiến thắng
        winPos.PlayWinEffect(); // Phát hiệu ứng chiến thắng
        RemoveAllStack();

        playerBody.localRotation = Quaternion.Euler(0 , -90f , 0) ; // Quay mặt player về hướng winpos
        winPos.OpenTreasure(); // Mở rương kho báu       
        playerAnimator.SetInteger(GameConstant.PlayerAnim , 2); // Chuyển sang animation chiến thắng
    }
    public void OnExitBridge(BridgeObject bridge)
    {
        bridge.SetColor(); // Kích hoạt lại collider của cầu khi rời khỏi
        bridge.boxCollider.enabled = false; // Vô hiệu hóa collider của cầu đã sử dụng
    }

}
