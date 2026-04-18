using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject deathPanel; // Kéo panel Game Over vào đây
    [SerializeField] public GameObject winPanel; // Kéo panel Victory vào đây
    [SerializeField] public TextMeshProUGUI stackCountText; // Kéo TextMeshPro hiển thị số stack vào đây\

    public void OnInit()
    {
        deathPanel.SetActive(false);
        winPanel.SetActive(false);
        UpdateStackCount(0); // Cập nhật số stack ban đầu là 0
    }
    public void UpdateStackCount(int count)
    {
        stackCountText.text = "Stack: " + count.ToString();
    }
    
}
