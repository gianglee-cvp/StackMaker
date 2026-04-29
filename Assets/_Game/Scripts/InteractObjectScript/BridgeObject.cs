using UnityEngine;

public class BridgeObject : PoolObject 

{
    public BoxCollider boxCollider ;
    public GameObject bridgeColor ; 
    public override void OnSpawn()
    {
        base.OnSpawn();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        boxCollider.enabled = true ;
        bridgeColor.SetActive(false) ;
    }
    public void SetColor()
    {
        bridgeColor.SetActive(true) ;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            GameManager.Instance.stackManager.HitBridge(boxCollider) ;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            GameManager.Instance.stackManager.OnExitBridge(this) ;
        }
    }
}