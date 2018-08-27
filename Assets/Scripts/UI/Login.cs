using DG.Tweening;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private LoginHelper helper = new LoginHelper();

    private void Awake()
    {
        MainThread.Init();
    }

    void Start()
    {
        helper.InitUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Messenger.AddListener("OnServerUnreachable", helper.OnServerUnreachable);

        Messenger.AddListener<JsonData>(S2C_Login.msgName, helper.OnLogin);
    }

    void OnDisable()
    {
        Messenger.RemoveListener("OnServerUnreachable", helper.OnServerUnreachable);

        Messenger.RemoveListener<JsonData>(S2C_Login.msgName, helper.OnLogin);
    }

    public void ClickPasswordLogin()
    {
        Messenger.AddListener("OnServerConnect", helper.PasswordLogin);

        if (Config.gsws.IsConnected())
        {
            helper.PasswordLogin();
        }
        else
        {
            Config.gsws.Connect();
        }
    }

    public void ClickSmsCodeLogin()
    {
        Messenger.AddListener("OnServerConnect", helper.SmsCodeLogin);

        if (Config.gsws.IsConnected())
        {
            helper.SmsCodeLogin();
        }
        else
        {
            Config.gsws.Connect();
        }
    }

    public void ClickRegister()
    {
        Messenger.AddListener("OnServerConnect", helper.Register);
    
        if (Config.gsws.IsConnected())
        {
            helper.Register();
        }
        else
        {
            Config.gsws.Connect();
        }
    }

    public void ClickQuit()
    {
        Util.Quit();
    }

    public void OnAccountEndEdit()
    {
        if (helper.AccountInput.text.Length > 0)
        {
            StartCoroutine(Util.HttpGet(Util.GetCheckAccountUrl(helper.AccountInput.text, "zh"), helper.CheckAccountCallback));
        }
    }
}
