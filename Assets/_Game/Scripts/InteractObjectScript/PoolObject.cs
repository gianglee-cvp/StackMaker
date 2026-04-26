
using UnityEngine;
public class PoolObject : MonoBehaviour
{
    public MapGenTag Gentag   ; 
    public Transform trans ;
    public GameObject obj ;
public virtual void OnSpawn()
{
}
public virtual void OnDespawn()
{
    trans.SetParent(null) ;
    obj.SetActive(false) ;
    ObjectPooler.Instance.ReturnObjToQueue(Gentag , this) ;
}
}