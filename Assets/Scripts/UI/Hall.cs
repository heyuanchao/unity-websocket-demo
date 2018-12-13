using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hall : MonoBehaviour
{
    private HallHelper helper = new HallHelper();

    // Use this for initialization
    void Start()
    {
        Utils.Log("Hall Start");
        helper.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Utils.Log("Hall OnEnable");

        Messenger.AddListener("OnServerDisonnect", helper.OnServerDisonnect);
        Messenger.AddListener("OnServerUnreachable", helper.OnServerUnreachable);

        Messenger.AddListener<JsonData>(S2C_Login.msgName, helper.OnLogin);

        Messenger.AddListener<JsonData>(S2C_Disconnect.msgName, helper.OnDisconnect);
        Messenger.AddListener<JsonData>(S2C_ShowTips.msgName, helper.OnShowTips);

        Messenger.AddListener<JsonData>(S2C_UpdateEggs.msgName, helper.OnUpdateEggs);
        Messenger.AddListener<JsonData>(S2C_UpdatePets.msgName, helper.OnUpdatePets);
        Messenger.AddListener<JsonData>(S2C_UpdateGoods.msgName, helper.OnUpdateGoods);
        Messenger.AddListener<JsonData>(S2C_UpdateHomes.msgName, helper.OnUpdateHomes);

        Messenger.AddListener<JsonData>(S2C_Buy.msgName, helper.OnBuy);
        Messenger.AddListener<JsonData>(S2C_Marquee.msgName, helper.OnMarquee);

        Messenger.AddListener<JsonData>(S2C_Brood.msgName, helper.OnBrood);
        Messenger.AddListener<JsonData>(S2C_Feed.msgName, helper.OnFeed);
    }

    void OnDisable()
    {
        Utils.Log("Hall OnDisable");

        //Messenger.RemoveListener("OnServerDisonnect", helper.OnServerDisonnect);
        //Messenger.RemoveListener("OnServerUnreachable", helper.OnServerUnreachable);

        //Messenger.RemoveListener<JsonData>(S2C_Login.msgName, helper.OnLogin);
        //Messenger.RemoveListener<JsonData>(S2C_Close.msgName, helper.OnClose);

        Messenger.Cleanup();
    }

    public void ClickLogout()
    {
        Utils.SetToken("");

        SceneManager.LoadScene("Login");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Global.gsws.Disconnect();
    }

    public void ClickGetFeedingPets()
    {
        helper.GetPets(0);
    }

    public void ClickGetMaturePets()
    {
        helper.GetPets(1);
    }

    public void ClickGetEggs()
    {
        helper.GetEggs();
    }

    public void ClickGetGoods()
    {
        helper.GetGoods();
    }

    public void ClickFeed()
    {
        if (Global.feedingPets.Count < 1)
        {
            return;
        }
        var pet = Global.feedingPets[0];
        helper.Feed(pet.id);
    }

    public void ClickBrood()
    {
        if (Global.eggs.Count < 1)
        {
            return;
        }
        var egg = Global.eggs[Random.Range(0, Global.eggs.Count)];
        helper.Brood(egg.id);
    }

    public void ClickBuy()
    {
        if (Global.goods.Count < 1)
        {
            return;
        }
        // helper.Buy(Random.Range(0, Global.goods.Count));
        helper.Buy(3);
    }

    public void ClickStorePet()
    {
        if (Global.feedingPets.Count < 1)
        {
            return;
        }
        var pet = Global.feedingPets[0];
        helper.Store(pet.id);
    }

    public void ClickSellFeedingPet()
    {
        if (Global.feedingPets.Count < 1)
        {
            return;
        }
        var pet = Global.feedingPets[0];
        helper.Sell(pet.id);
    }

    public void ClickSellPetsForSale()
    {
        if (Global.petsForSale.Count < 1)
        {
            return;
        }
        var pet = Global.petsForSale[0];
        helper.Sell(pet.id);
    }

    public void ClickGetFreeFeedTimeLeft()
    {
        if (Global.feedingPets.Count < 1)
        {
            return;
        }
        var pet = Global.feedingPets[0];
        helper.GetFreeFeedTimeLeft(pet.id);
    }

    public void ClickGetConsumptionSmsCode()
    {
        StartCoroutine(Utils.HttpGet(Config.GetConsumptionSmsCodeUrl(Global.account, "86"), helper.GetConsumptionSmsCodeCallback));
    }

    public void OnApplicationQuit()
    {
        Global.gsws.Disconnect();
    }

    public void ClickGetHomes()
    {
        helper.GetHomes();
    }
}
