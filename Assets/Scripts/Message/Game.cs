using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2S_GetPets
{
    public static readonly string msgName = "C2S_GetPets";
    private JsonData jsonData = new JsonData();

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
    private JsonData jsonData = new JsonData();

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
    private JsonData jsonData = new JsonData();

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