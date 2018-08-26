using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private Text TipsTxt;

    private void Awake()
    {
        MainThread.Init();
    }

    void Start()
    {
        TipsTxt = GameObject.Find("Canvas/Bottom_Group/Tips_Txt").GetComponent<Text>();
        TipsTxt.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Messenger.AddListener("OnServerUnreachable", OnServerUnreachable);

        Messenger.AddListener<JsonData>(S2C_Login.msgName, OnLogin);
    }

    void OnDisable()
    {
        Messenger.RemoveListener("OnServerUnreachable", OnServerUnreachable);

        Messenger.RemoveListener<JsonData>(S2C_Login.msgName, OnLogin);
    }

    public void ClickPasswordLogin()
    {
        Messenger.AddListener("OnServerConnect", passwordLogin);
       

        MyWebSocket.instance.Connect(Config.servAddr);
    }

    void passwordLogin()
    {
        Messenger.RemoveListener("OnServerConnect", passwordLogin);

        var loginMsg = new C2S_Login("15071334753", "123456");
        MyWebSocket.instance.SendMsg(loginMsg.jsonData);
    }

    public void ClickSmsLogin()
    {
        Messenger.AddListener("OnServerConnect", smsLogin);


        MyWebSocket.instance.Connect(Config.servAddr);
    }

    void smsLogin()
    {
        Messenger.RemoveListener("OnServerConnect", smsLogin);
    }

    public void ClickRegister()
    {
        Messenger.AddListener("OnServerConnect", register);


        MyWebSocket.instance.Connect(Config.servAddr);
    }

    void register()
    {
        Messenger.RemoveListener("OnServerConnect", register);

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
        MainThread.Run(() =>
        {
            SetText("无法连接服务器，请稍后重试");
            StartCoroutine(Util.DelayRun(() =>
            {
                SetText("");
            }, 3f));
        });
    }

    void SetText(string text)
    {
        TipsTxt.text = text;
    }

    void OnLogin(JsonData data)
    {
        Debug.Log("OnLogin: " + data.ToJson());
  
        MainThread.Run(() =>
        {
            SceneManager.LoadScene("Hall");
        });
    }
}
