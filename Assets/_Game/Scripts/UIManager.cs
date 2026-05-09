using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private TextMeshProUGUI stackCountText;
    [SerializeField] private TextMeshProUGUI levelText;
    private Dictionary<GameState, GameObject> uiPanels = new Dictionary<GameState, GameObject>();
    private GameState currentState = GameState.Base ;
    public void OnAwake()
    {
        uiPanels.Add(GameState.Home, homePanel);
        uiPanels.Add(GameState.Playing, playPanel);
        uiPanels.Add(GameState.Pause, pausePanel);
        uiPanels.Add(GameState.Win, winPanel);
        uiPanels.Add(GameState.Lose, deathPanel);
        deathPanel.SetActive(false);
        winPanel.SetActive(false);
        homePanel.SetActive(false);
        pausePanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void OnInit()
    {
        UpdateStackCount(0); // Cập nhật số stack ban đầu là 0
    }
    public void UpdateStackCount(int count)
    {
        stackCountText.text = "Stack: " + count.ToString();
    }
    public void UpdateStackSpriteMode(int count)
    {
        string scoreString =   count.ToString();
        stackCountText.text = "Stack: " ;
        foreach(char digit in scoreString)
        {
            stackCountText.text += $"<sprite name=\"{digit}\">";
        }
    }
    public void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level.ToString();
    }
    public void OnChangeUI(GameState state)
    {
        if(currentState == state)
        {
            return;
        } 
        else
        {
            if(state == GameState.Playing)
            {
                if(GameManager.Instance != null) GameManager.Instance.IsUIShow = false; 
            }
            else
            {
                if(GameManager.Instance != null) GameManager.Instance.IsUIShow = true; 
            }
            if(uiPanels.ContainsKey(currentState))
            {
                uiPanels[currentState].SetActive(false);
            }
            if(uiPanels.ContainsKey(state))
            {
                uiPanels[state].SetActive(true);
                }
            currentState = state;
        }

    }
    
}
