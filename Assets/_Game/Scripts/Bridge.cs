using System.Runtime.CompilerServices;
using UnityEngine;
public class Bridge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject bridgeColor ; 
    public void SetColor()
    {
        bridgeColor.SetActive(true);
    }

    public void UnsetColor()
    {
        if (bridgeColor != null) bridgeColor.SetActive(false);
    }
}
