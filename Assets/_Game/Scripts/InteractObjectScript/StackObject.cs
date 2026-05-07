using UnityEngine;

public class StackObject : PoolObject
{
 public Collider stackCollider;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag)){
            StackManager.Instance.AddStack( this);
        }
    }
 public override void OnSpawn()
    {
        base.OnSpawn();
        stackCollider.enabled = true; 
    }
}
