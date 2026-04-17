
using System.Numerics;

public enum CornerDirection
{
    UpLeft,     // 0 -> Góc 0
    UpRight, // 1 -> Góc 90
    DownRight,  // 2 -> Góc 180
    DownLeft   // 3 -> Góc 270
}
public enum BridgeDirection
{
    Vertical,    // 0 -> Góc 0 độ (Cầu dọc)
    Horizontal  // 1 -> Góc 90 độ (Cầu ngang)
}
public enum MoveDirection
{
    None ,
    Up , // 0 -> Lên
    Right,  // 1 -> Phải
    Down,   // 2 -> Xuống
    Left    // 3 -> Trái
}
