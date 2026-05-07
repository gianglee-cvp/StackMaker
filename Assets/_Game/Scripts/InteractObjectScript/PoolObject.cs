
using UnityEngine;
public class PoolObject : MonoBehaviour
{
    public MapGenTag Gentag   ; 
    public virtual void OnSpawn()
    {
    }
    public virtual void OnDespawn()
    {
        transform.SetParent(null) ;
        gameObject.SetActive(false) ;
        ObjectPooler.Instance.ReturnObjToQueue(Gentag , this) ;
    }
}