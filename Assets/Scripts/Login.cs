using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private bool loadHallScene;

    private Text TipsTxt;

    private Action<string> action;

    private void Awake()
    {
        UnityThread.initUnityThread();
    }
    void Start()
    {
        TipsTxt = GameObject.Find("Canvas/Tips_Txt").GetComponent<Text>();
        TipsTxt.text = "";

        action = SetText;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadHallScene)
        {
            SceneManager.LoadScene("Hall");
        }
    }

    void OnEnable()
    {
        Messenger.AddListener("OnServerUnreachable", OnServerUnreachable);

        Messenger.AddListener<JsonData>(S2C_Login.msgName, OnLogin);

        Debug.Log("Login OnEnable");
    }

    void OnDisable()
    {
        Messenger.RemoveListener("OnServerUnreachable", OnServerUnreachable);

        Messenger.RemoveListener<JsonData>(S2C_Login.msgName, OnLogin);

        Debug.Log("Login OnDisable");
    }

    public void ClickPasswordLogin()
    {
        Messenger.AddListener("OnServerConnect", OnClickPasswordLogin);
       

        MyWebSocket.instance.Connect(Config.servAddr);
    }

    void OnClickPasswordLogin()
    {
        Messenger.RemoveListener("OnServerConnect", OnClickPasswordLogin);

        var loginMsg = new C2S_Login("15071334753", "123456");
        MyWebSocket.instance.SendMsg(loginMsg.jsonData);
    }

    public void ClickSmsLogin()
    {
        Messenger.AddListener("OnServerConnect", OnClickSmsLogin);


        MyWebSocket.instance.Connect(Config.servAddr);
    }

    void OnClickSmsLogin()
    {
        Messenger.RemoveListener("OnServerConnect", OnClickSmsLogin);
    }

    public void ClickRegister()
    {
        Messenger.AddListener("OnServerConnect", OnClickRegister);


        MyWebSocket.instance.Connect(Config.servAddr);
    }

    void OnClickRegister()
    {
        Messenger.RemoveListener("OnServerConnect", OnClickRegister);

        Debug.Log("aaa");

        //var loginMsg = new C2S_Login("15071334753", "123456");
        //MyWebSocket.instance.SendMsg(loginMsg.jsonData);
    }

    public void ClickQuit()
    {
        Util.Quit();
    }

    void OnServerUnreachable()
    {
        Debug.Log("无法连接服务器，请稍后重试");
        //TipsTxt.text = "无法连接服务器，请稍后重试";

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            Debug.Log("This is executed from the main thread");
            SetText("无法连接服务器，请稍后重试");
            Invoke("SetText", 3f);
        });

        //UnityThread.executeInUpdate(() =>
        //{
        //    SetText("无法连接服务器，请稍后重试");
        //    Invoke("SetText", 3f);
        //});
    }

    void SetText(string text)
    {
        TipsTxt.text = text;
    }

    void OnLogin(JsonData data)
    {
        Debug.Log("OnLogin: " + data.ToJson());
        loadHallScene = true;

    }
}
