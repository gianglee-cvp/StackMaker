using System.Collections.Generic;
using System.Data.Common;
// using System.Numerics;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] CameraFollow cameraFollow; // Kéo Camera vào đây để gọi hàm cập nhật mốc khi số lượng gạch thay đổi
    public static StackManager Instance;
    public Transform stackHolder; // đối tượng cha chứa tất cả stack
    public Transform playerBody  ; 
    public float stackHeight = 0.3f ; 
    public List<GameObject> stackList = new List<GameObject>();    
    public int stackCount ; 
    public MoveDirection curMoveDirectionHitCorner = MoveDirection.None ; 

    [SerializeField] private Animator playerAnimator ;
    public void OnInit()
    {
            RemoveAllStack();
            playerAnimator.SetInteger("renwu" , 0); // Đặt lại animation về trạng thái ban đầu

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
    //    Debug.Log("Player Triggered: " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Stack")){
            stackList.Add(other.gameObject);
            other.transform.SetParent(stackHolder);
            other.transform.localPosition = new UnityEngine.Vector3(0 , stackHeight * stackCount - 0.5f , 0) ;  
            playerBody.localPosition += new UnityEngine.Vector3(0 , stackHeight , 0) ;
            stackCount++;
            other.gameObject.GetComponent<Collider>().enabled = false; // Vô hiệu hóa collider của gạch đã thu thập
            
            // Cập nhật mốc Camera khi số lượng gạch thay đổi
            if (cameraFollow != null)
            {
                cameraFollow.UpdateCameraMilestone(stackCount);
            }
        }
        else if (other.gameObject.CompareTag("Corner"))
        {
            PlayerController.Instance.hitCorner = true;
            Corner corner = other.gameObject.GetComponent<Corner>();
            // Debug.Log("PlayerController.Instance.curMoveDirection: " + PlayerController.Instance.curMoveDirection);
            
            playerAnimator.SetInteger("renwu" , 1);  

            if(PlayerController.Instance.curMoveDirection == MoveDirection.Up || PlayerController.Instance.curMoveDirection == MoveDirection.Down){
                curMoveDirectionHitCorner = corner.mustMoveHorizontal;
            }
            else if(PlayerController.Instance.curMoveDirection == MoveDirection.Left || PlayerController.Instance.curMoveDirection == MoveDirection.Right){
                curMoveDirectionHitCorner = corner.mustMoveVertical;
            }
        }
        else if (other.gameObject.CompareTag("Bridge"))
        {
           // Debug.Log("Player hit Bridge, removing one stack...");
            if(stackCount == 0) return ; // Nếu không còn stack nào thì không làm gì cả

            Destroy(stackList[stackCount- 1]);
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
        else if(other.gameObject.CompareTag("WinPos")){
            PlayerController.Instance.hitWinPos = true;

            GameManager.Instance.Point = stackCount; // Cập nhật điểm khi đến vị trí chiến thắng
            WinPosManager.Instance.PlayWinEffect(); // Phát hiệu ứng chiến thắng

            RemoveAllStack();

            playerBody.localRotation = Quaternion.Euler(0 , -90f , 0) ; // Quay mặt player về hướng winpos
            playerAnimator.SetInteger("renwu" , 2); // Chuyển sang animation chiến thắng

        }
        else if (other.gameObject.CompareTag("Gem"))
        {
            GameManager.Instance.GemCount++; // Cập nhật số lượng gem khi thu thập được
            Destroy(other.gameObject); // Hủy đối tượng gem sau khi thu thập
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Corner"))
        {
            playerAnimator.SetInteger("renwu" , 0);
        }
        if (other.gameObject.CompareTag("Bridge"))
        {
            other.gameObject.GetComponent<Bridge>().SetColor(); // Kích hoạt lại collider của cầu khi rời khỏi
            other.gameObject.GetComponent<Collider>().enabled = false; // Vô hiệu hóa collider của cầu đã sử dụng
        }


    }
    void RemoveAllStack()
    {
        foreach(GameObject stack in stackList){
            Destroy(stack);
        }
        stackList.Clear();
        stackCount = 0 ;
        playerBody.localPosition = new Vector3(0 , -0.3f , 0) ; // Đặt lại vị trí của player body về vị trí ban đầu
        playerBody.localRotation = Quaternion.Euler(0 , 90 , 0) ;
    }
}
