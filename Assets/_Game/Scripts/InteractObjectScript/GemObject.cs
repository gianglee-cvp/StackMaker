using UnityEngine;
public class GemObject : PoolObject
{
    public override void OnSpawn()
    {
        base.OnSpawn();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    public  void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            GameManager.Instance.stackManager.HitGem() ;
            gameObject.SetActive(false); // Vô hiệu hóa viên gem đã thu thập
        }
    }
}