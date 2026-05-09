using UnityEngine;

public class CornerObject : PoolObject
{
    public MoveDirection mustMoveHorizontal = MoveDirection.None ; 
    public MoveDirection mustMoveVertical = MoveDirection.None ; 
    [SerializeField] private Animator animator ;
    [SerializeField] private Collider cornerCollider ;
    private void OnEnable()
    {
        OnInit() ;
    }
    public void OnInit(){
        int yAngle = Mathf.RoundToInt(transform.rotation.eulerAngles.y) % 360; 
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
    private void OnTriggerEnter(Collider other)
    {
     if(other.CompareTag(GameConstant.PlayerTag)){
            animator.SetInteger(GameConstant.CornerAinm , 1);
            StackManager.Instance.HitCorner(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag)){
            animator.SetInteger(GameConstant.CornerAinm , 0);
            if (GameManager.Instance != null && GameManager.Instance.stackManager != null) GameManager.Instance.stackManager.OnExitGem() ;
        }

        
    }


}
