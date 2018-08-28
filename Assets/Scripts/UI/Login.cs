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
        MainThread.Init();
        helper.Init();
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

    public void ClickResetPassword()
    {

    }

    public void ClickQuit()
    {
        Utils.Quit();
    }

    public void OnAccountEndEdit()
    {
        if (helper.account.text.Length > 5)
        {
            StartCoroutine(Utils.HttpGet(Utils.GetCheckAccountUrl(helper.account.text, "zh"), helper.CheckAccountCallback));
        }
    }

    public void OnCountryChanged()
    {
        helper.SetMobileCode();
    }
}
