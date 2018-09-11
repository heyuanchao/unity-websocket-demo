using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg
{
    public string id;
    public int petType;
    public string name;
    public int maxFeedTimes; // 最大喂养次数
    public int feedOnceCost; // 单次喂养消耗
    public int reward; // 奖励

    public static List<Egg> ParseEggs(JsonData jd)
    {
        List<Egg> l = new List<Egg>();
        foreach (JsonData item in jd)
        {
            // Debug.Log(item.ToJson());
            Egg egg = new Egg
            {
                id = item["Id"].ToString(),
                petType = int.Parse(item["PetType"].ToString()),
                name = item["Name"].ToString(),
                maxFeedTimes = int.Parse(item["MaxFeedTimes"].ToString()),
                feedOnceCost = int.Parse(item["FeedOnceCost"].ToString()),
                reward = int.Parse(item["Reward"].ToString()),
            };

            l.Add(egg);
        }
        return l;
    }

    public override string ToString()
    {
        return "Id: " + id
            + ", PetType: " + petType
            + ", Name: " + name
            + ", MaxFeedTimes: " + maxFeedTimes
            + ", FeedOnceCost: " + feedOnceCost
            + ", Reward: " + reward;
    }
}

public class Pet
{
    public string id;
    public int petType;
    public string name;
    public int sameDayFeedTimes; // 同一天喂养次数
    public int feedTimes; // 喂养次数
    public int maxFeedTimes; // 最大喂养次数
    public int feedOnceCost;  // 单次喂养消耗
    public int reward; // 奖励

    public static List<Pet> ParsePets(JsonData jd)
    {
        List<Pet> l = new List<Pet>();
        foreach (JsonData item in jd)
        {
            // Debug.Log(item.ToJson());
            Pet pet = new Pet
            {
                id = item["Id"].ToString(),
                petType = int.Parse(item["PetType"].ToString()),
                name = item["Name"].ToString(),
                sameDayFeedTimes = int.Parse(item["SameDayFeedTimes"].ToString()),
                feedTimes = int.Parse(item["FeedTimes"].ToString()),
                maxFeedTimes = int.Parse(item["MaxFeedTimes"].ToString()),
                feedOnceCost = int.Parse(item["FeedOnceCost"].ToString()),
                reward = int.Parse(item["Reward"].ToString()),
            };

            l.Add(pet);
        }
        return l;
    }

    public override string ToString()
    {
        return "Id: " + id
            + ", PetType: " + petType
            + ", Name: " + name
            + ", SameDayFeedTimes: " + sameDayFeedTimes
            + ", FeedTimes: " + feedTimes
            + ", MaxFeedTimes: " + maxFeedTimes
            + ", FeedOnceCost: " + feedOnceCost
            + ", Reward: " + reward;
    }
}

public class Goods
{
    public int petType;
    public string name;
    public int maxFeedTimes; // 最大喂养次数
    public int feedOnceCost; // 单次喂养消耗
    public int reward; // 奖励
    public int price; // 售价
    public int amount; // 数量
    public bool hot; // 热门

    public static List<Goods> ParseGoods(JsonData jd)
    {
        List<Goods> l = new List<Goods>();
        foreach (JsonData item in jd)
        {
            // Debug.Log(item.ToJson());
            Goods goods = new Goods
            {
                petType = int.Parse(item["PetType"].ToString()),
                name = item["Name"].ToString(),
                maxFeedTimes = int.Parse(item["MaxFeedTimes"].ToString()),
                feedOnceCost = int.Parse(item["FeedOnceCost"].ToString()),
                reward = int.Parse(item["Reward"].ToString()),
                price = int.Parse(item["Price"].ToString()),
                amount = int.Parse(item["Amount"].ToString()),
                hot = bool.Parse(item["Hot"].ToString()),
            };

            l.Add(goods);
        }
        return l;
    }

    public override string ToString()
    {
        return "PetType: " + petType
            + ", Name: " + name
            + ", MaxFeedTimes: " + maxFeedTimes
            + ", FeedOnceCost: " + feedOnceCost
            + ", Reward: " + reward
            + ", Price: " + price
            + ", Amount: " + amount
            + ", Hot: " + hot;
    }
}