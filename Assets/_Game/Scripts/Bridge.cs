using System.Runtime.CompilerServices;
using UnityEngine;
public class Bridge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject bridgeColor ; 
    // void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         bridgeColor.SetActive(true);
    //     }

    // }
    public void SetColor()
    {
        Debug.Log("SetColor called on Bridge");
        bridgeColor.SetActive(true);
    }
}
