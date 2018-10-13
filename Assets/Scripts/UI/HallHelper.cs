using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HallHelper
{
    public InputField smsCode;
    public Toggle noPasswordPay;

    private Tips tips;

    public void Init()
    {
        smsCode = GameObject.Find("Canvas/Group3/SmsCode").GetComponent<InputField>();
        noPasswordPay = GameObject.Find("Canvas/Group3/NoPasswordPay").GetComponent<Toggle>();

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

    public void GetPets(int state)
    {
        Global.gsws.SendMsg(new C2S_GetPets().CreateGetPetsMsg(state));
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
        Global.gsws.SendMsg(new C2S_Feed().CreateFeedMsg(petId, smsCode.text, noPasswordPay.isOn));
    }

    public void Brood(string eggId)
    {
        Global.gsws.SendMsg(new C2S_Brood().CreateBroodMsg(eggId));
    }

    public void Buy(int petType)
    {
        Global.gsws.SendMsg(new C2S_Buy().CreateBuyMsg(petType, smsCode.text, noPasswordPay.isOn));
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
            // 需要弹出购买引导界面
        }
        else if (errCode == 3)
        {
            // 需要弹出获取消费验证码界面
            Debug.Log("请输入消费验证码");
        }
    }

    public void OnFeed(JsonData jd)
    {
        Debug.Log("OnFeed: " + jd.ToJson());
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
            return;
        }
        if (errCode == 2)
        {
            // 需要弹出购买引导界面
        }
        else if (errCode == 3)
        {
            // 需要弹出获取消费验证码界面
            Debug.Log("请输入消费验证码");
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

    public void GetConsumptionSmsCodeCallback(JsonData jd)
    {
        var errCode = int.Parse(jd["ErrCode"].ToString());
        var errMsg = jd["ErrMsg"].ToString();
        if (errMsg.Length > 0)
        {
            tips.Show(errMsg);
        }
    }

    public void Store(string petId)
    {
        Global.gsws.SendMsg(new C2S_Store().CreateStoreMsg(petId));
    }

    public void Sell(string petId)
    {
        Global.gsws.SendMsg(new C2S_Sell().CreateSellMsg(petId));
    }
}