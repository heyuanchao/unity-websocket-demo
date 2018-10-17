using DG.Tweening;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private LoginHelper helper = new LoginHelper();

    void Start()
    {
        Utils.Log("Login Start");
        MainThread.Init();
        helper.Init();

        ClickTokenLogin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Utils.Log("Login OnEnable");
        Messenger.AddListener("OnServerUnreachable", helper.OnServerUnreachable);

        Messenger.AddListener<JsonData>(S2C_Register.msgName, helper.OnRegister);
        Messenger.AddListener<JsonData>(S2C_Login.msgName, helper.OnLogin);
        Messenger.AddListener<JsonData>(S2C_Disconnect.msgName, helper.OnDisconnect);

        Messenger.AddListener<JsonData>(S2C_UpdatePets.msgName, helper.OnUpdatePets);
        Messenger.AddListener<JsonData>(S2C_Marquee.msgName, helper.OnMarquee);

        Messenger.AddListener<JsonData>(S2C_UpdateCoins.msgName, helper.OnUpdateCoins);
    }

    void OnDisable()
    {
        Utils.Log("Login OnDisable");
        //Messenger.RemoveListener("OnServerUnreachable", helper.OnServerUnreachable);
 
        //Messenger.RemoveListener<JsonData>(S2C_Register.msgName, helper.OnRegister);
        //Messenger.RemoveListener<JsonData>(S2C_Login.msgName, helper.OnLogin);
        Messenger.Cleanup();
    }

    public void ClickRegister()
    {
        StartCoroutine(Utils.HttpGet(helper.GetRegisterUrl(), helper.RegisterCallback));
        /*
        Messenger.AddListener("OnServerConnect", helper.Register);

        if (Global.gsws.IsConnected())
        {
            helper.Register();
        }
        else
        {
            Global.gsws.Connect();
        }
        */
    }

    public void ClickPasswordLogin()
    {
        Messenger.AddListener("OnServerConnect", helper.PasswordLogin);

        if (Global.gsws.IsConnected())
        {
            helper.PasswordLogin();
        }
        else
        {
            Global.gsws.Connect();
        }
    }

    public void ClickSmsCodeLogin()
    {
        Messenger.AddListener("OnServerConnect", helper.SmsCodeLogin);

        if (Global.gsws.IsConnected())
        {
            helper.SmsCodeLogin();
        }
        else
        {
            Global.gsws.Connect();
        }
    }

    public void ClickTokenLogin()
    {
        if (Global.account == "" || Global.token == "")
        {
            return;
        }

        Messenger.AddListener("OnServerConnect", helper.TokenLogin);

        if (Global.gsws.IsConnected())
        {
            helper.TokenLogin();
        }
        else
        {
            Global.gsws.Connect();
        }
    }

    public void ClickResetPassword()
    {
        StartCoroutine(Utils.HttpGet(Config.GetResetPasswordUrl(helper.account.text, helper.password.text, helper.smsCode.text, "zh"), helper.ResetPasswordCallback));
    }

    public void ClickQuit()
    {
        Utils.Quit();
    }

    public void OnAccountEndEdit()
    {
        if (helper.mobileCode.text == "0086" && helper.account.text.Length == 11 || helper.account.text.Length > 6)
        {
            StartCoroutine(Utils.HttpGet(Config.GetCheckAccountUrl(helper.account.text, "zh"), helper.CheckAccountCallback));
        }
    }

    public void OnCountryChanged()
    {
        helper.SetMobileCode();
    }

    public void ClickGetRegistrationSmsCode()
    {
        if (helper.mobileCode.text == "0086" && helper.account.text.Length == 11 || helper.account.text.Length > 6)
        {
            StartCoroutine(Utils.HttpGet(Config.GetRegistrationSmsCodeUrl(helper.account.text, "86"), helper.GetRegistrationSmsCodeCallback));
        }
    }

    public void ClickGetServerList()
    {
        StartCoroutine(Utils.HttpGet(Config.serverListUrl, helper.GetServerListCallback));
    }
}
