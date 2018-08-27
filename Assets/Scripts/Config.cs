public class Config
{
    private const string localServAddr = "192.168.8.103";
    private const string remoteServAddr = "120.77.205.210";

    private const string gameServAddr = localServAddr;
    // public const string gameServAddr = remoteServAddr;

    public const string gameServWSAddr = "ws://" + gameServAddr + ":8000";
    public const string gameServHTTPAddr = "http://" + gameServAddr + ":8001";

    public const string checkAccountAddr = gameServHTTPAddr + "/check";

    public static MyWebSocket gsws = new MyWebSocket(gameServWSAddr); // 游戏服 WebSocket
}
