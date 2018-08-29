﻿public class Config
{
    private const string localServHost = "192.168.8.109";
    private const string remoteServHost = "game.hedr.top";

    private const string gameServHost = localServHost;
    // public const string gameServHost = remoteServHost;

    public const string gameServWSAddr = "ws://" + gameServHost + ":8000";
    public const string gameServHTTPAddr = "http://" + gameServHost + ":8001";

    public const string checkAccountAddr = gameServHTTPAddr + "/check";
    public const string resetPasswordAddr = gameServHTTPAddr + "/reset";

    public static MyWebSocket gsws = new MyWebSocket(gameServWSAddr); // 游戏服 WebSocket
}
