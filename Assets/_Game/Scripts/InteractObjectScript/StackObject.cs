using UnityEngine;

public class StackObject : PoolObject
{
 public Collider stackCollider;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag)){
            //Debug.Log("Player hit stack at position: " + transform.position);   
            StackManager.Instance.AddStack( this);
        }
    }
 public override void OnSpawn()
    {
        base.OnSpawn();
        stackCollider.enabled = true; 
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
}
