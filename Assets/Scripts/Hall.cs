using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hall : MonoBehaviour
{
    private bool loadLoginScene;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (loadLoginScene)
        {
            SceneManager.LoadScene("Login");
        }
    }

    void OnEnable()
    {
        Messenger.AddListener("OnServerConnect", OnServerConnect);
        Messenger.AddListener("OnServerDisonnect", OnServerDisonnect);
        Messenger.AddListener("OnServerUnreachable", OnServerUnreachable);

        Debug.Log("Hall OnEnable");
    }

    void OnDisable()
    {
        Messenger.RemoveListener("OnServerConnect", OnServerConnect);
        Messenger.RemoveListener("OnServerDisonnect", OnServerDisonnect);
        Messenger.RemoveListener("OnServerUnreachable", OnServerUnreachable);

        Debug.Log("Hall OnDisable");
    }

    void OnServerConnect()
    {

    }

    void OnServerDisonnect()
    {
        MyWebSocket.instance.Connect(Config.servAddr);
    }

    void OnServerUnreachable()
    {
        // todo 弹出错误提示，点击确定切换到登录界面
        Debug.Log("无法连接服务器，请稍后重试");
        loadLoginScene = true;
    }
}
