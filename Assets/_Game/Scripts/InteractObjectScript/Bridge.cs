using System.Runtime.CompilerServices;
using UnityEngine;
public class Bridge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject bridgeColor ; 
    public Collider bridgeCollider ;
    public void OnEnable()
    {
        bridgeColor.SetActive(false);
        bridgeCollider.enabled = true;
    }
    public void SetColor()
    {
        bridgeColor.SetActive(true);
    }

    public void UnsetColor()
    {
        if (bridgeColor != null) bridgeColor.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            StackManager.Instance.HitBridge(bridgeCollider);
        }
    }
}
