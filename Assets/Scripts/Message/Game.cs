using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 服务器定义 http://git.hedr.top:3000/heyuanchao/FeedPetServer/src/master/msg/game.go
 */
public class S2C_UpdateCoins
{
    public static readonly string msgName = "S2C_UpdateCoins";
}

public class C2S_GetPets
{
    public static readonly string msgName = "C2S_GetPets";
    private readonly JsonData jsonData = new JsonData();

    public JsonData CreateGetPetsMsg()
    {
        jsonData[msgName] = JsonMapper.ToObject("{}");
        return jsonData;
    }
}

public class S2C_UpdatePets
{
    public static readonly string msgName = "S2C_UpdatePets";
}

public class C2S_GetEggs
{
    public static readonly string msgName = "C2S_GetEggs";
    private readonly JsonData jsonData = new JsonData();

    public JsonData CreateGetEggsMsg()
    {
        jsonData[msgName] = JsonMapper.ToObject("{}");
        return jsonData;
    }
}

public class S2C_UpdateEggs
{
    public static readonly string msgName = "S2C_UpdateEggs";
}

public class C2S_GetGoods
{
    public static readonly string msgName = "C2S_GetGoods";
    private readonly JsonData jsonData = new JsonData();

    public JsonData CreateGetGoodsMsg()
    {
        jsonData[msgName] = JsonMapper.ToObject("{}");
        return jsonData;
    }
}

public class S2C_UpdateGoods
{
    public static readonly string msgName = "S2C_UpdateGoods";
}

public class C2S_Feed
{
    public static readonly string msgName = "C2S_Feed";
    private readonly JsonData jsonData = new JsonData();

    public JsonData CreateFeedMsg(string petId, string smsCode, bool noPassword)
    {
        var jd = new JsonData();
        jd["PetId"] = petId;
        jd["SmsCode"] = smsCode;
        jd["NoPassword"] = noPassword;

        jsonData[msgName] = jd;
        return jsonData;
    }
}

public class S2C_Feed
{
    public static readonly string msgName = "S2C_Feed";
}

public class S2C_UpdateFeedTimes
{
    public static readonly string msgName = "S2C_UpdateFeedTimes";
}

public class C2S_GetFreeFeedTimeLeft
{
    public static readonly string msgName = "C2S_GetFreeFeedTimeLeft";

    private readonly JsonData jsonData = new JsonData();

    public JsonData CreateGetFreeFeedTimeLeftMsg(string petId)
    {
        var jd = new JsonData();
        jd["PetId"] = petId;

        jsonData[msgName] = jd;
        return jsonData;
    }
}

public class S2C_UpdateFreeFeedTimeLeft
{
    public static readonly string msgName = "S2C_UpdateFreeFeedTimeLeft";
}

public class C2S_Brood
{
    public static readonly string msgName = "C2S_Brood";
    private readonly JsonData jsonData = new JsonData();

    public JsonData CreateBroodMsg(string eggId)
    {
        var jd = new JsonData();
        jd["EggId"] = eggId;

        jsonData[msgName] = jd;
        return jsonData;
    }
}

public class S2C_Brood
{
    public static readonly string msgName = "S2C_Brood";
}

public class C2S_Buy
{
    public static readonly string msgName = "C2S_Buy";
    private readonly JsonData jsonData = new JsonData();

    public JsonData CreateBuyMsg(int petType, string smsCode, bool noPassword)
    {
        var jd = new JsonData();
        jd["PetType"] = petType;
        jd["SmsCode"] = smsCode;
        jd["NoPassword"] = noPassword;

        jsonData[msgName] = jd;
        return jsonData;
    }
}

public class S2C_Buy
{
    public static readonly string msgName = "S2C_Buy";
}

public class S2C_UpdateGoodsAmount
{
    public static readonly string msgName = "S2C_UpdateGoodsAmount";
}
