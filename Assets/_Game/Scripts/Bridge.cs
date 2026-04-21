using System.Runtime.CompilerServices;
using UnityEngine;
public class Bridge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject bridgeColor ; 
    public void SetColor()
    {
        Debug.Log("SetColor called on Bridge");
        bridgeColor.SetActive(true);
    }
}
