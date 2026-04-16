using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class MapEditorWindow : EditorWindow
{
    public enum PaintBrush
    {
        Base,
        Wall,
        Stack,
        Corner,
        Bridge,
        Z_Eraser_All
    }

    private class EditorCell
    {
        public bool isBase;
        public bool isWall;
        public bool isStack;
        public bool isCorner;
        public CornerDirection cornerDir;
        public bool isBridge;
        public BridgeDirection bridgeDir;

        public void Clear()
        {
            isBase = false; isWall = false; isStack = false;
            isCorner = false; isBridge = false;
        }

        public bool IsEmpty()
        {
            return !isBase && !isWall && !isStack && !isCorner && !isBridge;
        }
    }

    private string levelFilePath = "Assets/_Game/StreamingAssets/Levels/level1.json";
    
    // Kích thước thật của map ghi vào JSON
    private int jsonRows = 100;
    private int jsonCols = 100;

    // Viewport Camera (Chống Lag)
    private int viewCenterRow = 0;
    private int viewCenterCol = 0;
    private int viewRadius = 12; // Vẽ 25x25 ô = 625 buttons = Cực mượt không lag!

    private Dictionary<Vector2Int, EditorCell> mapData = new Dictionary<Vector2Int, EditorCell>();

    private PaintBrush currentBrush = PaintBrush.Base;
    private CornerDirection brushCornerDir = CornerDirection.UpLeft;
    private BridgeDirection brushBridgeDir = BridgeDirection.Horizontal;

    private Vector2 scrollPosition;

    [MenuItem("Tools/Pro Map Editor")]
    public static void ShowWindow()
    {
        GetWindow<MapEditorWindow>("Pro Map Editor").Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("CÀI ĐẶT BẢN ĐỒ", EditorStyles.boldLabel);
        levelFilePath = EditorGUILayout.TextField("Đường dẫn JSON", levelFilePath);
        
        jsonRows = EditorGUILayout.IntField("Size Thật: Số Hàng (Rows)", jsonRows);
        jsonCols = EditorGUILayout.IntField("Size Thật: Số Cột (Columns)", jsonCols);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("1. TẠO MAP TRỐNG MỚI", GUILayout.Height(30))) { mapData.Clear(); }
        GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("2. ĐỌC MAP TỪ JSON", GUILayout.Height(30))) { LoadMap(); }
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("3. LƯU MAP (JSON)", GUILayout.Height(30))) { SaveMap(); }
        GUI.backgroundColor = Color.white;
        GUILayout.EndHorizontal();

        DrawBrushMenu();
        
        // --- CAMERA Panning để chống lag ---
        GUILayout.Space(10);
        GUILayout.Label("CAMERA (PAN MÀN HÌNH CHỐNG LAG)", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        viewRadius = EditorGUILayout.IntSlider("Độ Rộng Camera", viewRadius, 5, 25);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("◀ Sang Trái")) viewCenterCol += 5; // Do flip 180 nên cộng trừ có thể đảo
        if (GUILayout.Button("Sang Phải ▶")) viewCenterCol -= 5;
        if (GUILayout.Button("▲ Lên Trên")) viewCenterRow += 5;
        if (GUILayout.Button("▼ Xuống Dưới")) viewCenterRow -= 5;
        if (GUILayout.Button("Trở Về Tâm (0,0)")) { viewCenterRow = 0; viewCenterCol = 0; }
        GUILayout.EndHorizontal();

        DrawGrid();
    }

    private void DrawBrushMenu()
    {
        GUILayout.Space(10);
        GUILayout.Label("CÔNG CỤ VẼ (BRUSH)", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        currentBrush = (PaintBrush)EditorGUILayout.EnumPopup("Chọn Loại Cọ:", currentBrush, GUILayout.Width(300));
        GUILayout.EndHorizontal();

        if (currentBrush == PaintBrush.Corner)
            brushCornerDir = (CornerDirection)EditorGUILayout.EnumPopup("Hướng Góc:", brushCornerDir, GUILayout.Width(300));
        else if (currentBrush == PaintBrush.Bridge)
            brushBridgeDir = (BridgeDirection)EditorGUILayout.EnumPopup("Hướng Cầu:", brushBridgeDir, GUILayout.Width(300));
    }

    private void DrawGrid()
    {
        GUILayout.Space(10);
        GUILayout.Label("MA TRẬN LƯỚI (CHUỘT TRÁI: Dán, CHUỘT PHẢI: Xóa. CÓ THỂ KÉO BÔI)", EditorStyles.boldLabel);
        
        int rMax = viewCenterRow + viewRadius;
        int rMin = viewCenterRow - viewRadius;
        int cMax = viewCenterCol + viewRadius;
        int cMin = viewCenterCol - viewRadius;

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int r = rMax; r >= rMin; r--)
        {
            GUILayout.BeginHorizontal();
            for (int c = cMax; c >= cMin; c--)
            {
                Vector2Int pos = new Vector2Int(r, c);
                EditorCell cell = GetCell(pos);
                
                string cellText = GetCellDisplayString(cell);
                GUI.backgroundColor = GetCellColor(cell, r, c);

                Rect rect = GUILayoutUtility.GetRect(40, 40);
                
                // Vẽ ô vuông thay cho Button
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.fontStyle = FontStyle.Bold;
                GUI.Box(rect, cellText, style);

                Event e = Event.current;
                if ((e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && rect.Contains(e.mousePosition))
                {
                    bool isErase = (e.button == 1); // 1 = Chuột phải
                    ApplyBrush(cell, isErase);
                    if (cell.IsEmpty()) mapData.Remove(pos);
                    e.Use(); // Ăn event để vẽ mượt khi kéo
                }
            }
            GUILayout.EndHorizontal();
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndScrollView();
    }

    private EditorCell GetCell(Vector2Int pos)
    {
        if (!mapData.ContainsKey(pos)) mapData[pos] = new EditorCell();
        return mapData[pos];
    }

    private void ApplyBrush(EditorCell cell, bool isErase)
    {
        if (currentBrush == PaintBrush.Z_Eraser_All)
        {
            cell.Clear();
            return;
        }

        if (isErase)
        {
            switch (currentBrush)
            {
                case PaintBrush.Base: cell.isBase = false; break;
                case PaintBrush.Wall: cell.isWall = false; break;
                case PaintBrush.Stack: cell.isStack = false; break;
                case PaintBrush.Corner: cell.isCorner = false; break;
                case PaintBrush.Bridge: cell.isBridge = false; break;
            }
        }
        else
        {
            switch (currentBrush)
            {
                case PaintBrush.Base: cell.isBase = true; break;
                case PaintBrush.Wall: cell.isWall = true; break;
                case PaintBrush.Stack: cell.isStack = true; break;
                case PaintBrush.Corner: cell.isCorner = true; cell.cornerDir = brushCornerDir; break;
                case PaintBrush.Bridge: cell.isBridge = true; cell.bridgeDir = brushBridgeDir; break;
            }
        }
    }

    private string GetCellDisplayString(EditorCell cell)
    {
        string t = "";
        if (cell.isBase) t += "B\n";
        if (cell.isWall) t += "W\n";
        if (cell.isStack) t += "S\n";
        if (cell.isCorner) t += "C(" + cell.cornerDir.ToString().Substring(0,1) + ")\n";
        if (cell.isBridge) t += "Br(" + cell.bridgeDir.ToString().Substring(0,1) + ")\n";
        return t.TrimEnd('\n');
    }

    private Color GetCellColor(EditorCell cell, int r, int c)
    {
        if (r == 0 && c == 0 && cell.IsEmpty()) return Color.black; 
        if (cell.IsEmpty()) return new Color(0.8f, 0.8f, 0.8f);
        if (cell.isBridge) return Color.cyan;
        if (cell.isCorner) return new Color(1f, 0.5f, 0f);
        if (cell.isStack) return Color.blue;
        if (cell.isWall) return Color.red;
        if (cell.isBase) return Color.green;
        return Color.white;
    }

    private void LoadMap()
    {
        if (!File.Exists(levelFilePath)) { Debug.LogError("Không tìm thấy file JSON"); return; }
        
        string json = File.ReadAllText(levelFilePath);
        LevelDataWrapper wrapper = JsonUtility.FromJson<LevelDataWrapper>(json);
        if (wrapper == null || wrapper.level == null) return;

        jsonRows = wrapper.level.rows;
        jsonCols = wrapper.level.columns;
        mapData.Clear();
        var l = wrapper.level;

        if (l.baseData != null) foreach(var d in l.baseData) GetCell(new Vector2Int(d.row, d.column)).isBase = true;
        if (l.wallData != null) foreach(var d in l.wallData) GetCell(new Vector2Int(d.row, d.column)).isWall = true;
        if (l.stackData != null) foreach(var d in l.stackData) GetCell(new Vector2Int(d.row, d.column)).isStack = true;
        if (l.cornerData != null) foreach(var d in l.cornerData) {
            var cell = GetCell(new Vector2Int(d.row, d.column)); cell.isCorner = true; cell.cornerDir = d.direction;
        }
        if (l.bridgeData != null) foreach(var d in l.bridgeData) {
            var cell = GetCell(new Vector2Int(d.row, d.column)); cell.isBridge = true; cell.bridgeDir = d.direction;
        }
    }

    private void SaveMap()
    {
        LevelDataWrapper wrapper = new LevelDataWrapper();
        wrapper.level = new LevelData();

        if (File.Exists(levelFilePath))
        {
            string oldJson = File.ReadAllText(levelFilePath);
            LevelDataWrapper oldWrapper = JsonUtility.FromJson<LevelDataWrapper>(oldJson);
            if(oldWrapper != null && oldWrapper.level != null) wrapper.level = oldWrapper.level;
        }

        wrapper.level.rows = jsonRows;
        wrapper.level.columns = jsonCols;
        wrapper.level.baseData.Clear(); wrapper.level.wallData.Clear();
        wrapper.level.stackData.Clear(); wrapper.level.cornerData.Clear(); wrapper.level.bridgeData.Clear();

        foreach (var kvp in mapData)
        {
            int r = kvp.Key.x; int c = kvp.Key.y; EditorCell cell = kvp.Value;
            if (cell.isBase) wrapper.level.baseData.Add(new BaseData { row = r, column = c });
            if (cell.isWall) wrapper.level.wallData.Add(new WallData { row = r, column = c });
            if (cell.isStack) wrapper.level.stackData.Add(new StackData { row = r, column = c });
            if (cell.isCorner) wrapper.level.cornerData.Add(new CornerData { row = r, column = c, direction = cell.cornerDir });
            if (cell.isBridge) wrapper.level.bridgeData.Add(new BridgeData { row = r, column = c, direction = cell.bridgeDir });
        }

        File.WriteAllText(levelFilePath, JsonUtility.ToJson(wrapper, true));
        AssetDatabase.Refresh();
        Debug.Log("Lưu Map thành công!");
    }
}
