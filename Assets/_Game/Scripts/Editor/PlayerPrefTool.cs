using UnityEngine;
using UnityEditor;

public class EditorTools
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Đã xóa sạch bộ nhớ PlayerPrefs!");
    }
}