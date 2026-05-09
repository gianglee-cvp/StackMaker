public static class GameConstant
{
    public const string PlayerAnim = "renwu";
    public const string CornerAinm = "zhuanjiaoSet";

    public const string PlayerTag = "Player";
    public const string StackTag = "Stack";
    public const string BridgeTag = "Bridge";

    public const int AnimIdle = 0;
    public const int AnimRun = 1;
    public const int AnimWin = 2;

    public const float RaycastMaxDistance = 50f;
    public const float MoveStopThreshold = 0.01f;

    public const float StackBaseYOffset = -0.5f;
    public const float PlayerBodyDefaultY = -0.3f;

    public const float CornerRotationStep = 90f;
    public const float WinPosRotationY = -90f;
    public const float PlayerBodyDefaultRotY = 90f;
}