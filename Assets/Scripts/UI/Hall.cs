using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hall : MonoBehaviour
{
    private HallHelper helper = new HallHelper();

    // Use this for initialization
    void Start()
    {
        Utils.Log("Hall Start");
        helper.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Utils.Log("Hall OnEnable");

        Messenger.AddListener("OnServerDisonnect", helper.OnServerDisonnect);
        Messenger.AddListener("OnServerUnreachable", helper.OnServerUnreachable);

        Messenger.AddListener<JsonData>(S2C_Login.msgName, helper.OnLogin);
        Messenger.AddListener<JsonData>(S2C_Close.msgName, helper.OnClose);
    }

    void OnDisable()
    {
        Utils.Log("Hall OnDisable");

        Messenger.RemoveListener("OnServerDisonnect", helper.OnServerDisonnect);
        Messenger.RemoveListener("OnServerUnreachable", helper.OnServerUnreachable);

        Messenger.RemoveListener<JsonData>(S2C_Login.msgName, helper.OnLogin);
    }

    public void ClickLogout()
    {
        Utils.SetToken("");

        SceneManager.LoadScene("Login");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Global.gsws.Disconnect();
    }
}
