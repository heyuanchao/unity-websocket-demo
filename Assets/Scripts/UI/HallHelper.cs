using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HallHelper
{
    private Tips tips = new Tips();

    public void Init()
    {

    }

    public void TokenLogin()
    {
        Messenger.RemoveListener("OnServerConnect", TokenLogin);

        if (Global.account == "" || Global.token == "")
        {
            MainThread.Run(() =>
            {
                SceneManager.LoadScene("Login");
            });
            return;
        }

        Global.gsws.SendMsg(new C2S_Login().CreateTokenLoginMsg(Global.account, Global.token, "zh"));
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
                SceneManager.LoadScene("Login");
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

            // SceneManager.LoadScene("Hall");
        });
    }

    public void OnServerUnreachable()
    {
        Debug.Log("无法连接服务器");
        MainThread.Run(() =>
        {
            Utils.SetToken("");
            SceneManager.LoadScene("Login");
        });
    }

    public void OnServerDisonnect()
    {
        if (Global.account == "" || Global.token == "")
        {
            MainThread.Run(() =>
            {
                SceneManager.LoadScene("Login");
            });
            return;
        }
        Messenger.AddListener("OnServerConnect", TokenLogin);
        Global.gsws.Connect();
    }

    public void OnClose(JsonData jd)
    {
        Debug.Log("OnClose: " + jd.ToJson());
        var errMsg = jd["ErrMsg"].ToString();
        Global.gsws.closed = true;
        tips.Show(errMsg, () =>
        {
            SceneManager.LoadScene("Login");
        });
    }
}