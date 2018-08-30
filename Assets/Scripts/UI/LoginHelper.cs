using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginHelper
{
    public Dropdown country;
    public Text mobileCode;

    public InputField account;
    public InputField password;
    public InputField smsCode;
    public InputField invitationCode;

    private GameObject tips;
    private Text tipsText;


    public void Init()
    {
        country = GameObject.Find("Canvas/Center_Group/Account_Group/Country").GetComponent<Dropdown>();
        mobileCode = GameObject.Find("Canvas/Center_Group/Account_Group/Account/MobileCode").GetComponent<Text>();

        account = GameObject.Find("Canvas/Center_Group/Account_Group/Account").GetComponent<InputField>();
        password = GameObject.Find("Canvas/Center_Group/Password").GetComponent<InputField>();
        smsCode = GameObject.Find("Canvas/Center_Group/SmsCode_Group/SmsCode").GetComponent<InputField>();
        invitationCode = GameObject.Find("Canvas/Center_Group/SmsCode_Group/SmsCode").GetComponent<InputField>();

        tips = GameObject.Find("Canvas/Tips");
        tipsText = GameObject.Find("Canvas/Tips/Text").GetComponent<Text>();
        tips.SetActive(false);

        initAccount();
        initCountry();
    }

    private void initAccount()
    {
        var acc = Utils.GetAccount();
        if (acc.Length > 0)
        {
            account.text = acc;
        }
    }

    private void initCountry()
    {
        var options = new List<Dropdown.OptionData>();
        var codes = MobileCode.GetCodes();
        foreach (string key in codes.Keys)
        {
            options.Add(new Dropdown.OptionData(key));
        }

        country.AddOptions(options);
    }

    public void ShowTips(string text)
    {
        MainThread.Run(() =>
        {
            tips.SetActive(true);
            tipsText.text = text;
            Utils.DelayRun2(3f, () =>
            {
                tips.SetActive(false);
            });
        });
    }

    public void SetMobileCode()
    {
        var codes = MobileCode.GetCodes();
        var key = country.options[country.value].text;
        mobileCode.text = codes[key];
    }

    public void PasswordLogin()
    {
        Messenger.RemoveListener("OnServerConnect", PasswordLogin);

        Config.gsws.SendMsg(new C2S_Login().CreatePasswordLoginMsg(account.text, password.text, "zh"));
    }

    public void SmsCodeLogin()
    {
        Messenger.RemoveListener("OnServerConnect", SmsCodeLogin);

        Config.gsws.SendMsg(new C2S_Login().CreateSmsCodeLoginMsg(account.text, smsCode.text, "zh"));
    }

    public void Register()
    {
        Messenger.RemoveListener("OnServerConnect", Register);

        Config.gsws.SendMsg(new C2S_Register().CreateRegisterMsg(account.text, password.text, smsCode.text, invitationCode.text, "zh"));
    }

    public void OnRegister(JsonData jd)
    {
        Debug.Log("OnRegister: " + jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            ShowTips(errMsg);
        }
    }

    public void OnLogin(JsonData jd)
    {
        Debug.Log("OnLogin: " + jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            ShowTips(errMsg);
            return;
        }
        if (errCode == 0)
        {
            MainThread.Run(() =>
            {
                Utils.SetAccount(account.text);
                SceneManager.LoadScene("Hall");
            });
        }
    }

    public void OnServerUnreachable()
    {
        Debug.Log("无法连接服务器，请稍后重试");
        ShowTips("无法连接服务器，请稍后重试");
    }

    public void CheckAccountCallback(JsonData jd)
    {
        // Debug.Log(jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            ShowTips(errMsg);
        }
    }

    public void ResetPasswordCallback(JsonData jd)
    {
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            ShowTips(errMsg);
        }
    }
}
