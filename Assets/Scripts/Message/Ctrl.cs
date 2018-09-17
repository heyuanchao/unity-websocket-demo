using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 服务器定义 http://git.hedr.top:3000/heyuanchao/FeedPetServer/src/master/msg/control.go
 */
public class S2C_ShowTips
{
    public static readonly string msgName = "S2C_ShowTips";
}

public class S2C_Disconnect
{
    public static readonly string msgName = "S2C_Disconnect";
}

public class S2C_Marquee
{
    public static readonly string msgName = "S2C_Marquee";
}

public class Ping
{
    public static readonly string msgName = "Ping";
    private readonly JsonData jsonData = new JsonData();

    public JsonData CreatePingMsg()
    {
        jsonData[msgName] = JsonMapper.ToObject("{}");
        return jsonData;
    }
}

public class Pong
{
    public static readonly string msgName = "Pong";
}
