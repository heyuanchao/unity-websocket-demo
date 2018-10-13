using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static string account = "";
    public static string token = "";

    public static MyWebSocket gsws = new MyWebSocket(Config.gameServWSAddr); // 游戏服 WebSocket

    public static List<Egg> eggs = new List<Egg>();

    public static List<Pet> feedingPets = new List<Pet>();
    public static List<Pet> petsForSale = new List<Pet>();

    public static List<Goods> goods = new List<Goods>();

    public static List<string> marquees = new List<string>();
}
