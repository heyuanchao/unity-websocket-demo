public class Config
{
    private const string localServAddr = "ws://192.168.8.103:8000";
    private const string remoteServAddr = "ws://120.77.205.210:8000";

    public const string gameServAddr = localServAddr;
    // public const string servAddr = remoteServAddr;

    public static MyWebSocket gsws = new MyWebSocket(gameServAddr); // 游戏服 WebSocket
}
