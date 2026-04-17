using System.Runtime.CompilerServices;
using UnityEngine;
public class Bridge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject bridgeColor ; 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bridgeColor.SetActive(true);
        }

    }
}
