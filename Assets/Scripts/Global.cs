using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static string account = "";
    public static string token = "";

    public static MyWebSocket gsws = new MyWebSocket(Config.gameServWSAddr); // 游戏服 WebSocket
}
