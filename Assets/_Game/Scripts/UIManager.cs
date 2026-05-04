using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject deathPanel; // Kéo panel Game Over vào đây
    [SerializeField] public GameObject winPanel; // Kéo panel Victory vào đây
    [SerializeField] public GameObject homePanel;
    [SerializeField] public GameObject pausePanel;
    [SerializeField] public GameObject playPanel;
    [SerializeField] public TextMeshProUGUI stackCountText; // Kéo TextMeshPro hiển thị số stack vào đây\
    [SerializeField] public TextMeshProUGUI levelText; // Kéo TextMeshPro hiển thị level vào đây
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
            // Debug.LogWarning("UI is already in state: " + state);
            return;
        } 
        else
        {
            if(state == GameState.Playing)
            {
                GameManager.Instance.IsUIShow = false; // Ẩn UI khi vào trạng thái Playing
            }
            else
            {
                GameManager.Instance.IsUIShow = true; // Hiển thị UI khi vào trạng thái khác Playing
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
