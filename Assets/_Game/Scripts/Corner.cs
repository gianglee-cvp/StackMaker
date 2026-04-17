using UnityEngine;

public class Corner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MoveDirection mustMoveHorizontal = MoveDirection.None ; // xác định khi chạm vào góc này thì phải di chuyển theo hướng nào để không bị kẹt
    public MoveDirection mustMoveVertical = MoveDirection.None ; // xác định khi chạm vào góc này thì phải di chuyển theo hướng nào để không bị kẹt
    [SerializeField] private Animator animator ; 
    void Start()
    {
        int yAngle = Mathf.RoundToInt(transform.rotation.eulerAngles.y) % 360; // Lấy góc quay quanh trục Y và đảm bảo nó nằm trong khoảng 0-359
        switch(yAngle){
            case 0 :
                mustMoveHorizontal = MoveDirection.Right ;
                mustMoveVertical = MoveDirection.Down ;
                break;
            case 90 :
                mustMoveHorizontal = MoveDirection.Left ;
                mustMoveVertical = MoveDirection.Down ;
                break;
            case 180 :
                mustMoveHorizontal = MoveDirection.Left ;
                mustMoveVertical = MoveDirection.Up ;
                break;
            case 270 :
                mustMoveHorizontal = MoveDirection.Right ;
                mustMoveVertical = MoveDirection.Up ;
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
     if(other.gameObject.CompareTag("Player")){
            animator.SetInteger("zhuanjiaoSet" , 1);
        }
    }
    void OnTriggerExit(Collider other)
    {
        animator.SetInteger("zhuanjiaoSet" , 0);

    }

}
