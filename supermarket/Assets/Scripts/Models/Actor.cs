using Pathfinding;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor
{
    public ulong uid;
    public string nickname;
    public string avatarUrl;

    public GameObject avatar;

    public ChatBubble chatBubble;

    public Dictionary<string, int> goods = new();

    public float expireTime;

    public void Chat(string content)
    {
        if (chatBubble != null)
        {
            chatBubble.ShowChatMessage(content);
        }
    }

    public void Hangout()
    {

    }

    public void PayBill()
    {
        float total = 0;
        foreach (var item in goods)
        {
            total += ConfigManager.Instance.goodsPriceConfig[item.Key] * item.Value;
        }
        
    }

    public void GotoArea(string area)
    {
        
    }
}
