using UnityEngine;

public class WinPosManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject closeTreasure;
    public GameObject openTreasure;

    [SerializeField] private ParticleSystem winEffect;
    public static WinPosManager Instance;
    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Instance = this  ; 
        }
    }

    void Start()
    {
        closeTreasure.SetActive(true);
        openTreasure.SetActive(false);
    }
    void OnEnable()
    {
        GameManager.OnChange += HandleGameStateChange;
    }
    void OnDisable()
    {
        GameManager.OnChange -= HandleGameStateChange;
    }
    private void HandleGameStateChange(string state)
    {
        if (state == "Win")
        {
            closeTreasure.SetActive(false);
            openTreasure.SetActive(true);
        }
        else if (state == "Restart")
        {
            closeTreasure.SetActive(true);
            openTreasure.SetActive(false);
        }
    }
    public void PlayWinEffect()
    {
        if (winEffect != null)
        {
            winEffect.Play();
        }
    }


}
