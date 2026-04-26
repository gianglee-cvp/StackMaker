using UnityEngine;

public class WinPosObject : PoolObject
{
    public GameObject closeTreasure;
    public GameObject openTreasure;
    [SerializeField] private ParticleSystem winEffect;
    public override void OnSpawn()
    {
        base.OnSpawn();
        closeTreasure.SetActive(true);
        openTreasure.SetActive(false);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    public void OpenTreasure()
    {
        closeTreasure.SetActive(false);
        openTreasure.SetActive(true);
    }
    public void PlayWinEffect()
    {
        if (winEffect != null)
        {
            winEffect.Play();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.stackManager.OnHitWinPos(this) ; 
        }
    }

}