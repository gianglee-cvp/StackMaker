using UnityEngine;

public class BridgeObject : PoolObject 

{
    public BoxCollider boxCollider ;
    [SerializeField] private GameObject bridgeColor ; 

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
            if (GameManager.Instance != null && GameManager.Instance.stackManager != null) GameManager.Instance.stackManager.HitBridge() ;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            if (GameManager.Instance != null && GameManager.Instance.stackManager != null) GameManager.Instance.stackManager.OnExitBridge(this) ;
        }
    }
}