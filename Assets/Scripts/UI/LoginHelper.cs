using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginHelper
{
    private Text tips;
    public InputField account;
    public Text mobileCode;
    public Dropdown country;

    public void Init()
    {
        tips = GameObject.Find("Canvas/Bottom_Group/Tips").GetComponent<Text>();
        account = GameObject.Find("Canvas/Center_Group/Account_Group/Account").GetComponent<InputField>();
        mobileCode = GameObject.Find("Canvas/Center_Group/Account_Group/Account/MobileCode").GetComponent<Text>();
        country = GameObject.Find("Canvas/Center_Group/Account_Group/Country").GetComponent<Dropdown>();

        initCountry();
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

    public void SetTips(string text)
    {
        tips.text = text;
    }

    public void SetMobileCode()
    {
        var codes = MobileCode.GetCodes();
        var key = country.options[country.value].text;
        mobileCode.text = codes[key];
    }

    public void CheckAccountCallback(JsonData jd)
    {
        Debug.Log(jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errCode > 0)
        {
            SetTips(errMsg);
            Utils.DelayRun2(3f, () =>
            {
                SetTips("");
            });
        }
    }

    public void PasswordLogin()
    {
        Messenger.RemoveListener("OnServerConnect", PasswordLogin);

        Config.gsws.SendMsg(new C2S_Login().CreatePasswordLoginMsg("15071334753", "123456"));
    }

    public void SmsCodeLogin()
    {
        Messenger.RemoveListener("OnServerConnect", SmsCodeLogin);

        Config.gsws.SendMsg(new C2S_Login().CreateSmsCodeLoginMsg("15071334753", "6666"));
    }

    public void Register()
    {
        Messenger.RemoveListener("OnServerConnect", Register);

        Config.gsws.SendMsg(new C2S_Register().CreateRegisterMsg("15071334753", "123456", "6666", "10001"));
    }

    public void OnLogin(JsonData data)
    {
        Debug.Log("OnLogin: " + data.ToJson());

        MainThread.Run(() =>
        {
            SceneManager.LoadScene("Hall");
        });
    }

    public void OnServerUnreachable()
    {
        Debug.Log("无法连接服务器，请稍后重试");
        MainThread.Run(() =>
        {
            SetTips("无法连接服务器，请稍后重试");
            Utils.DelayRun2(3f, () =>
            {
                SetTips("");
            });
        });
    }
}
