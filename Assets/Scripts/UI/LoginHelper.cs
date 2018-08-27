using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginHelper
{

    private Text TipsTxt;
    public InputField AccountInput;

    public void InitUI()
    {
        TipsTxt = GameObject.Find("Canvas/Bottom_Group/Tips_Txt").GetComponent<Text>();
        TipsTxt.text = "";

        AccountInput = GameObject.Find("Canvas/Account_Group/Account_Input").GetComponent<InputField>();
    }

    public void SetText(string text)
    {
        TipsTxt.text = text;
    }

    public void CheckAccountCallback(JsonData jd)
    {
        Debug.Log(jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errCode > 0)
        {
            SetText(errMsg);
            Util.DelayRun2(3f, () =>
            {
                SetText("");
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
            SetText("无法连接服务器，请稍后重试");
            Util.DelayRun2(3f, () =>
            {
                SetText("");
            });
        });
    }
}
