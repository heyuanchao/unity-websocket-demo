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

        Messenger.AddListener<JsonData>(S2C_Close.msgName, helper.OnClose);
        Messenger.AddListener<JsonData>(S2C_Show.msgName, helper.OnShow);

        Messenger.AddListener<JsonData>(S2C_UpdateEggs.msgName, helper.OnUpdateEggs);
        Messenger.AddListener<JsonData>(S2C_UpdatePets.msgName, helper.OnUpdatePets);
        Messenger.AddListener<JsonData>(S2C_UpdateGoods.msgName, helper.OnUpdateGoods);

        Messenger.AddListener<JsonData>(S2C_Buy.msgName, helper.OnBuy);
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

    public void ClickGetPets()
    {
        helper.GetPets();
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
        if (Global.pets.Count < 1)
        {
            return;
        }
        Pet pet = (Pet)Global.pets[0];
        helper.Feed(pet.id);
    }

    public void ClickBrood()
    {
        if (Global.eggs.Count < 1)
        {
            return;
        }
        Egg egg = (Egg)Global.eggs[0];
        helper.Brood(egg.id);
    }

    public void ClickBuy()
    {
        if (Global.goods.Count < 1)
        {
            return;
        }
        helper.Buy(Random.Range(0, Global.goods.Count));
    }

    public void ClickGetFreeFeedTimeLeft()
    {
        if (Global.pets.Count < 1)
        {
            return;
        }
        Pet pet = (Pet)Global.pets[0];
        helper.GetFreeFeedTimeLeft(pet.id);
    }
}
