using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HallHelper
{
    private Tips tips;

    public void Init()
    {
        tips = new Tips();
    }

    public void TokenLogin()
    {
        Messenger.RemoveListener("OnServerConnect", TokenLogin);

        if (Global.account == "" || Global.token == "")
        {
            MainThread.Run(() =>
            {
                Debug.Log("token 无效");
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
        tips.Show("无法连接服务器", () =>
        {
            Utils.SetToken("");
            SceneManager.LoadScene("Login");
        });
    }

    public void OnServerDisonnect()
    {
        if (Global.account == "" || Global.token == "")
        {
            Debug.Log("与服务器断开连接");
            MainThread.Run(() =>
            {
                SceneManager.LoadScene("Login");
            });
            return;
        }
        Messenger.AddListener("OnServerConnect", TokenLogin);
        Global.gsws.Connect();
    }

    public void OnDisconnect(JsonData jd)
    {
        Debug.Log("OnDisconnect: " + jd.ToJson());
        var errMsg = jd["Msg"].ToString();
        Global.gsws.closed = true;
        tips.Show(errMsg, () =>
        {
            SceneManager.LoadScene("Login");
        });
    }

    public void OnShowTips(JsonData jd)
    {
        Debug.Log("OnShowTips: " + jd.ToJson());
        var errMsg = jd["Msg"].ToString();
        tips.Show(errMsg);
    }

    public void GetPets()
    {
        Global.gsws.SendMsg(new C2S_GetPets().CreateGetPetsMsg());
    }

    public void GetEggs()
    {
        Global.gsws.SendMsg(new C2S_GetEggs().CreateGetEggsMsg());
    }

    public void GetGoods()
    {
        Global.gsws.SendMsg(new C2S_GetGoods().CreateGetGoodsMsg());
    }

    public void OnUpdateEggs(JsonData jd)
    {
        Global.eggs = Egg.ParseEggs(jd["Eggs"]);
        foreach (Egg egg in Global.eggs)
        {
            Debug.Log(egg.ToString());
        }
    }

    public void OnUpdatePets(JsonData jd)
    {
        Global.pets = Pet.ParsePets(jd["Pets"]);
        foreach (Pet pet in Global.pets)
        {
            Debug.Log(pet.ToString());
        }
    }

    public void OnUpdateGoods(JsonData jd)
    {
        Global.goods = Goods.ParseGoods(jd["Goods"]);
        foreach (Goods g in Global.goods)
        {
            Debug.Log(g.ToString());
        }
    }

    public void Feed(string petId)
    {
        Global.gsws.SendMsg(new C2S_Feed().CreateFeedMsg(petId));
    }

    public void Brood(string eggId)
    {
        Global.gsws.SendMsg(new C2S_Brood().CreateBroodMsg(eggId));
    }

    public void Buy(int pos)
    {
        Global.gsws.SendMsg(new C2S_Buy().CreateBuyMsg(pos));
    }

    public void GetFreeFeedTimeLeft(string petId)
    {
        Global.gsws.SendMsg(new C2S_GetFreeFeedTimeLeft().CreateGetFreeFeedTimeLeftMsg(petId));
    }

    public void OnBuy(JsonData jd)
    {
        Debug.Log("OnBuy: " + jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
            return;
        }
        if (errCode == 2)
        {
            // SWC 余额不足，需要弹出购买引导
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

    public void OnBrood(JsonData jd)
    {
        Debug.Log("OnBrood: " + jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
            return;
        }
    }
}