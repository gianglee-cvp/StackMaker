using UnityEngine;
public class GemObject : PoolObject
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            GameManager.Instance.stackManager.HitGem() ;
            gameObject.SetActive(false); 
        }
    }
}