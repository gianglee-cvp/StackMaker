using UnityEngine;

public class WinPosTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private ParticleSystem winEffect;
    public void PlayWinEffect(){
        if(winEffect != null){
            winEffect.Play();
        }
    }
}
