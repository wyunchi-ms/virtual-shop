using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Collections.Concurrent;
using UnityEngine.Networking;
using Pathfinding;

public class ActorController : MonoBehaviour
{
    private ActorStatus status;

    public Dictionary<string, int> goods = new();

    void Start()
    {

    }

    void Update()
    {

    }

    public void Hangout()
    {

    }

    public void PayBill()
    {
        var goodsPriceConfig = ConfigManager.Instance.goodsPriceConfig;
        float total = 0;
        foreach (var item in goods)
        {
            foreach (var typeInfo in goodsPriceConfig.Values)
            {
                if (typeInfo.ContainsKey(item.Key))
                {
                    total += typeInfo[item.Key] * item.Value;
                }
            }
        }
        
    }

    public void GotoArea(GoodsType goodsType)
    {
        var pointers = GameObject.FindGameObjectsWithTag(goodsType.ToString());
        AIDestinationSetter setter = this.gameObject.GetComponent<AIDestinationSetter>();
        setter.target = pointers[Random.Range(0, pointers.Count)].GetComponent<Transform>();
        status = ActorStatus.Walk;
    }
}