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

    private Tips tips;

    public void Init()
    {
        country = GameObject.Find("Canvas/Center_Group/Account_Group/Country").GetComponent<Dropdown>();
        mobileCode = GameObject.Find("Canvas/Center_Group/Account_Group/Account/MobileCode").GetComponent<Text>();

        account = GameObject.Find("Canvas/Center_Group/Account_Group/Account").GetComponent<InputField>();
        password = GameObject.Find("Canvas/Center_Group/Password").GetComponent<InputField>();
        smsCode = GameObject.Find("Canvas/Center_Group/SmsCode_Group/SmsCode").GetComponent<InputField>();
        invitationCode = GameObject.Find("Canvas/Center_Group/SmsCode_Group/SmsCode").GetComponent<InputField>();

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

        Global.gsws.SendMsg(new C2S_Login().CreatePasswordLoginMsg(account.text, password.text, "zh"));
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

        Global.gsws.SendMsg(new C2S_Register().CreateRegisterMsg(account.text, password.text, smsCode.text, invitationCode.text, "zh"));
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

    public void OnClose(JsonData jd)
    {
        Debug.Log("OnClose: " + jd.ToJson());
        var errMsg = jd["ErrMsg"].ToString();
        Global.gsws.closed = true;
        tips.Show(errMsg);
    }

    public void CheckAccountCallback(JsonData jd)
    {
        // Debug.Log(jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }

    public void ResetPasswordCallback(JsonData jd)
    {
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }

    public void OnUpdatePets(JsonData jd)
    {
        Global.pets.Clear();

        foreach (JsonData item in jd["Pets"])
        {
            // Debug.Log(item.ToJson());
            Pet pet = new Pet
            {
                id = item["Id"].ToString(),
                petType = int.Parse(item["PetType"].ToString()),
                name = item["Name"].ToString(),
                icon = item["Icon"].ToString(),
                sameDayFeedTimes = int.Parse(item["SameDayFeedTimes"].ToString()),
                feedTimes = int.Parse(item["FeedTimes"].ToString()),
                maxFeedTimes = int.Parse(item["MaxFeedTimes"].ToString()),
                feedOnceCost = int.Parse(item["FeedOnceCost"].ToString()),
                reward = int.Parse(item["Reward"].ToString()),
            };

            Global.pets.Add(pet);
        }

        foreach (Pet pet in Global.pets)
        {
            Debug.Log(pet.ToString());
        }
    }
}
