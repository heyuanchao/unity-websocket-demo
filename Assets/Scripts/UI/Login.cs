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

        if (Config.gsws.IsConnected())
        {
            passwordLogin();
        }
        else
        {
            Config.gsws.Connect();
        }

    }

    void passwordLogin()
    {
        Messenger.RemoveListener("OnServerConnect", passwordLogin);

        Config.gsws.SendMsg(new C2S_Login().CreatePasswordLoginMsg("15071334753", "123456"));
    }

    public void ClickSmsCodeLogin()
    {
        Messenger.AddListener("OnServerConnect", smsCodeLogin);

        if (Config.gsws.IsConnected())
        {
            smsCodeLogin();
        }
        else
        {
            Config.gsws.Connect();
        }
    }

    void smsCodeLogin()
    {
        Messenger.RemoveListener("OnServerConnect", smsCodeLogin);

        Config.gsws.SendMsg(new C2S_Login().CreateSmsCodeLoginMsg("15071334753", "6666"));
    }

    public void ClickRegister()
    {
        Messenger.AddListener("OnServerConnect", register);

        if (Config.gsws.IsConnected())
        {
            register();
        }
        else
        {
            Config.gsws.Connect();
        }


    }

    void register()
    {
        Messenger.RemoveListener("OnServerConnect", register);

        Config.gsws.SendMsg(new C2S_Register().CreateRegisterMsg("15071334753", "123456", "6666", "10001"));
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
