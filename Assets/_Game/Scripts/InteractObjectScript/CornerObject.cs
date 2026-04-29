using UnityEngine;

public class CornerObject : PoolObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MoveDirection mustMoveHorizontal = MoveDirection.None ; // xác định khi chạm vào góc này thì phải di chuyển theo hướng nào để không bị kẹt
    public MoveDirection mustMoveVertical = MoveDirection.None ; // xác định khi chạm vào góc này thì phải di chuyển theo hướng nào để không bị kẹt
    [SerializeField] private Animator animator ;
    public Collider cornerCollider ;
    public void Start()
    {
        OnInit() ; 
    }
    void OnEnable()
    {
      //  Debug.Log("Corner nEnable called") ;
        OnInit() ;
    }
    public void OnInit(){
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
     if(other.CompareTag(GameConstant.PlayerTag)){
            animator.SetInteger(GameConstant.CornerAinm , 1);
            StackManager.Instance.HitCorner(this);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag)){
            animator.SetInteger(GameConstant.CornerAinm , 0);
            GameManager.Instance.stackManager.OnExitGem() ;
        }

        
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }

}
