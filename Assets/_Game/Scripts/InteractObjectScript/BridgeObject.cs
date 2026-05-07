using UnityEngine;

public class BridgeObject : PoolObject 

{
    public BoxCollider boxCollider ;
    public GameObject bridgeColor ; 

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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            GameManager.Instance.stackManager.HitBridge(boxCollider) ;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            GameManager.Instance.stackManager.OnExitBridge(this) ;
        }
    }
}