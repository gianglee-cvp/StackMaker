using UnityEngine;

public class WinPosObject : PoolObject
{
    [SerializeField] private GameObject closeTreasure;
    [SerializeField] private GameObject openTreasure;
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            if (GameManager.Instance != null && GameManager.Instance.stackManager != null) GameManager.Instance.stackManager.OnHitWinPos(this) ; 
        }
    }

}