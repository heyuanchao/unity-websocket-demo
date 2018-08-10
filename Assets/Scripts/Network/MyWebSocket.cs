using LitJson;
using System;
using System.Text;
using UnityEngine;
using WebSocketSharp;

public class MyWebSocket : MonoBehaviour
{
    public static MyWebSocket instance;
    private WebSocket ws;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }

    private bool isConnected()
    {
        if (ws != null && ws.IsConnected)
            return true;
        return false;
    }

    public void Connect(string servAddr)
    {
        if (isConnected()) return;
        Debug.Log("aaa");
        ws = new WebSocket(Config.servAddr);

        ws.OnOpen += OnWebSocketOpen;
        ws.OnClose += OnWebSocketClose;
        ws.OnMessage += OnMessageReceived;

        ws.Connect();
    }

    public void Disconnect()
    {
        if (isConnected())
        {
            Debug.Log("bbb");
            ws.Close();
        }
        ws = null;
    }

    private void OnWebSocketOpen(object sender, EventArgs e)
    {

    }

    private void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket Closed: " + e.Code + " " + e.Reason);
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
