using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

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
    }
}
