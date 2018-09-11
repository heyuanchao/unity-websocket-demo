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