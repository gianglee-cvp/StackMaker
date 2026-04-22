using UnityEngine;
using UnityEditor;

public class EditorTools
{
    // Tạo một menu mới tên là "Tools" trên thanh trên cùng của Unity
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Đã xóa sạch bộ nhớ PlayerPrefs!");
    }
}