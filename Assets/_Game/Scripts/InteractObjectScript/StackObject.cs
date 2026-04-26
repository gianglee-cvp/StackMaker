using UnityEngine;

public class StackObject : PoolObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 public Collider stackCollider;
 public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
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
