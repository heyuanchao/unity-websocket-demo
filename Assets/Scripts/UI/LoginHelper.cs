﻿using LitJson;
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

    private Tips tips;

    public void Init()
    {
        country = GameObject.Find("Canvas/Center_Group/Account_Group/Country").GetComponent<Dropdown>();
        mobileCode = GameObject.Find("Canvas/Center_Group/Account_Group/Account/MobileCode").GetComponent<Text>();

        account = GameObject.Find("Canvas/Center_Group/Account_Group/Account").GetComponent<InputField>();
        password = GameObject.Find("Canvas/Center_Group/Password").GetComponent<InputField>();
        smsCode = GameObject.Find("Canvas/Center_Group/SmsCode_Group/SmsCode").GetComponent<InputField>();
        invitationCode = GameObject.Find("Canvas/Center_Group/InvitationCode").GetComponent<InputField>();

        InitAccountAndToken();
        InitCountry();

        tips = new Tips();
    }

    private void InitAccountAndToken()
    {
        Global.account = Utils.GetAccount();
        Global.token = Utils.GetToken();

        if (Global.account.Length > 0)
        {
            account.text = Global.account;
        }
    }

    private void InitCountry()
    {
        var options = new List<Dropdown.OptionData>();
        var codes = MobileCode.GetCodes();
        foreach (string key in codes.Keys)
        {
            options.Add(new Dropdown.OptionData(key));
        }

        country.AddOptions(options);
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

        Global.gsws.SendMsg(new C2S_Login().CreatePasswordLoginMsg(mobileCode.text.Substring(2), account.text, password.text, "zh"));
    }

    public void SmsCodeLogin()
    {
        Messenger.RemoveListener("OnServerConnect", SmsCodeLogin);

        Global.gsws.SendMsg(new C2S_Login().CreateSmsCodeLoginMsg(account.text, smsCode.text, "zh"));
    }

    public void TokenLogin()
    {
        Messenger.RemoveListener("OnServerConnect", TokenLogin);

        if (Global.account != "" && Global.token != "")
        {
            Global.gsws.SendMsg(new C2S_Login().CreateTokenLoginMsg(Global.account, Global.token, "zh"));
        }
    }

    public void Register()
    {
        Messenger.RemoveListener("OnServerConnect", Register);

        Global.gsws.SendMsg(new C2S_Register().CreateRegisterMsg(mobileCode.text.Substring(2), account.text, password.text, smsCode.text, invitationCode.text, "zh"));
    }

    public string GetRegisterUrl()
    {
        return Config.GetRegisterUrl(mobileCode.text.Substring(2), account.text, password.text, password.text, smsCode.text, invitationCode.text, "zh");
    }

    public void OnRegister(JsonData jd)
    {
        Debug.Log("OnRegister: " + jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }

    public void OnLogin(JsonData jd)
    {
        Debug.Log("OnLogin: " + jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
            return;
        }
        if (errCode == 0)
        {
            OnLoginSuccessful(jd);
            return;
        }
        var loginType = int.Parse(jd["LoginType"].ToString());
        if (loginType == 2) // Token 登录
        {
            Utils.Log("Token登录失败");
            MainThread.Run(() =>
            {
                Utils.SetToken("");
            });
            return;
        }
    }

    private void OnLoginSuccessful(JsonData jd)
    {
        MainThread.Run(() =>
        {
            Utils.SetAccount(jd["Account"].ToString());
            Utils.SetToken(jd["Token"].ToString());

            SceneManager.LoadScene("Hall");
        });
    }

    public void OnServerUnreachable()
    {
        Debug.Log("无法连接服务器，请稍后重试");
        tips.Show("无法连接服务器，请稍后重试");
    }

    public void OnDisconnect(JsonData jd)
    {
        Debug.Log("OnDisconnect: " + jd.ToJson());
        var errMsg = jd["Msg"].ToString();
        Global.gsws.closed = true;
        tips.Show(errMsg);
    }

    public void CheckAccountCallback(JsonData jd)
    {
        if (jd == null)
        {
            Debug.Log("请求失败，请稍后重试");
            return;
        }
        // Debug.Log(jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }

    public void GetRegistrationSmsCodeCallback(JsonData jd)
    {
        if (jd == null)
        {
            Debug.Log("请求失败，请稍后重试");
            return;
        }
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }

    public void ResetPasswordCallback(JsonData jd)
    {
        if (jd == null)
        {
            Debug.Log("请求失败，请稍后重试");
            return;
        }
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }

    public void OnUpdatePets(JsonData jd)
    {
        int state = int.Parse(jd["State"].ToJson());
        var pets = Pet.ParsePets(jd["Pets"]);
        if (state == 0) // 喂养状态
        {
            Global.feedingPets = pets;
        }
        else if (state == 1) // 待售状态
        {
            Global.petsForSale = pets;
        }

        foreach (Pet pet in pets)
        {
            Debug.Log(pet.ToString());
        }
    }

    public void OnMarquee(JsonData jd)
    {

        Global.marquees.Add(jd["Msg"].ToString());
        foreach (var m in Global.marquees)
        {
            Debug.Log(m);
        }
        Global.marquees.Clear();
    }

    public void OnUpdateCoins(JsonData jd)
    {
        Debug.Log(jd["Coins"].ToString());
        Debug.Log(float.Parse(jd["Coins"].ToString()));
    }

    public void GetServerListCallback(JsonData jd)
    {
        if (jd == null)
        {
            Debug.Log("请求失败，请稍后重试");
            return;
        }
        if (!jd.IsArray)
        {
            Debug.Log("服务端返回数据异常");
            return;
        }
        for (int i = 0; i < jd.Count; i++)
        {
            Debug.Log(jd[i].ToJson());
        }
    }

    public void GetVersionCallback(JsonData jd)
    {
        if (jd == null)
        {
            tips.Show("检测新版本失败，请检查网络后重试");
            return;
        }
        Debug.Log(jd.ToJson());
    }

    public void RegisterCallback(JsonData jd)
    {
        if (jd == null)
        {
            Debug.Log("请求失败，请稍后重试");
            return;
        }
        Debug.Log(jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }
}
