using UnityEngine;

public class Stack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 public Collider stackCollider;
 public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            //Debug.Log("Player hit stack at position: " + transform.position);   
            StackManager.Instance.AddStack(stackCollider);
        }
    }
}
