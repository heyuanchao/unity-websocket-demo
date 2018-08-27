using LitJson;
using System;
using System.Text;
using UnityEngine;
using WebSocketSharp;

public class MyWebSocket
{
    private WebSocket ws;
    private string servAddr;
    private bool opened;

    public MyWebSocket(string servAddr)
    {
        this.servAddr = servAddr;
    }

    public bool IsConnected()
    {
        if (ws == null)
        {
            return false;
        }
        return ws.IsConnected;
    }

    public void Connect()
    {
        if (IsConnected())
        {
            return;
        }
        ws = new WebSocket(servAddr);

        ws.OnOpen += OnWebSocketOpen;
        ws.OnClose += OnWebSocketClose;
        ws.OnMessage += OnMessageReceived;

        ws.ConnectAsync();
    }

    public void Disconnect()
    {
        if (IsConnected())
        {
            ws.Close();
        }
        opened = false;
        ws = null;
    }

    private void OnWebSocketOpen(object sender, EventArgs e)
    {
        opened = true;
        Messenger.Broadcast("OnServerConnect");
    }

    private void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket Closed: " + e.Code + " " + e.Reason + ", Opened: " + opened);
        ws = null;
        if (opened)
        {
            opened = false;
            Messenger.Broadcast("OnServerDisonnect");
        }
        else
        {
            Messenger.Broadcast("OnServerUnreachable");
        }
    }

    private void OnMessageReceived(object sender, MessageEventArgs e)
    {
        var msg = Encoding.UTF8.GetString(e.RawData);
        Debug.Log("OnMessageReceived: " + msg);
        var data = JsonMapper.ToObject(msg);
        foreach (string msgName in data.Keys)
        {
            // Debug.Log(msgName + " : " + data[msgName].ToJson());
            Messenger.Broadcast<JsonData>(msgName, data[msgName]);
        }
    }

    public void SendMsg(JsonData data)
    {
        Util.Log("Send: " + data.ToJson());
        ws.Send(data.ToJson());
    }
}
