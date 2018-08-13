using LitJson;
using System;
using System.Text;
using UnityEngine;
using WebSocketSharp;

public class MyWebSocket : MonoBehaviour
{
    public static MyWebSocket instance;

    private WebSocket ws;
    private string servAddr;
    private bool opened;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }

    private bool IsConnected()
    {
        if (ws != null && ws.IsConnected)
        {
            return true;
        }
        return false;
    }

    public void Connect(string servAddr)
    {
        if (ws != null || IsConnected())
        {
            return;
        }
        this.servAddr = servAddr;
        ws = new WebSocket(servAddr);

        ws.OnOpen += OnWebSocketOpen;
        ws.OnClose += OnWebSocketClose;
        ws.OnMessage += OnMessageReceived;

        ws.Connect();
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
        Debug.Log("WebSocket Closed: " + e.Code + " " + e.Reason + " " + opened);
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
        Debug.Log("OnBinaryMessageReceived: " + msg);
        var jd = JsonMapper.ToObject(msg);
        foreach (string msgName in jd.Keys)
        {
            Debug.Log(msgName + ":" + jd[msgName].ToJson());
        }
    }

}
