using UnityEngine;
public class GemObject : PoolObject
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            if (GameManager.Instance != null && GameManager.Instance.stackManager != null) GameManager.Instance.stackManager.HitGem() ;
            gameObject.SetActive(false); 
        }
    }
}