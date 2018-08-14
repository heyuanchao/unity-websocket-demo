using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private Text TipsTxt;
    // Use this for initialization
    void Start()
    {
        TipsTxt = GameObject.Find("Canvas/Tips_Txt").GetComponent<Text>();
        TipsTxt.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Messenger.AddListener("OnServerConnect", OnServerConnect);
        Messenger.AddListener("OnServerUnreachable", OnServerUnreachable);

        Debug.Log("Login OnEnable");
    }

    void OnDisable()
    {
        Messenger.RemoveListener("OnServerConnect", OnServerConnect);
        Messenger.RemoveListener("OnServerUnreachable", OnServerUnreachable);

        Debug.Log("Login OnDisable");
    }

    void OnServerConnect()
    {
        SceneManager.LoadScene("Hall");
    }

    void OnServerUnreachable()
    {
        Debug.Log("无法连接服务器，请稍后重试");
        TipsTxt.text = "无法连接服务器，请稍后重试";
        Invoke("ClearText", 3f);
    }

    void ClearText()
    {
        TipsTxt.text = "";
    }
}
