using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json.Serialization;

public class DanmuConsumer : MonoBehaviour
{
    private IModel channel;
    private EventingBasicConsumer consumer;
    private string consumerTag;

    private JsonSerializerSettings deserializerSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    // Start is called before the first frame update
    void Start()
    {
        ConnectionFactory factory = new()
        {
            UserName = "wyunchi",
            Password = "wyunchi",
            VirtualHost = "/",
            HostName = "localhost",
            Port = 5672
        };

        IConnection conn = factory.CreateConnection();
        channel = conn.CreateModel();

        consumer = new EventingBasicConsumer(channel);
        consumer.Received += (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            channel.BasicAck(ea.DeliveryTag, false);
            string str = Encoding.UTF8.GetString(body);
            Dictionary<string, object> record = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
            string actionType = (string)record.GetValueOrDefault("action_type");

            if (actionType == ActionTypeConstants.ChatType)
            {
                ChatMessage chatMessage = JsonConvert.DeserializeObject<ChatMessage>(str, deserializerSettings);
                Actor actor = ActorManager.Instance.GetActor(chatMessage.UserId);
                if (actor == null)
                {
                    ActorManager.Instance.AddActor(chatMessage.UserId, chatMessage.Nickname, chatMessage.Avatar);
                }
                else
                {
                    if (chatMessage.Content.Contains("闲逛"))
                    {
                        actor.Hangout();
                    }
                    else
                    {
                        actor.Chat(chatMessage.Content);
                        if (chatMessage.Content.Contains("结账"))
                        {
                            actor.PayBill();
                        }
                    }
                }
            }
            else if (actionType == ActionTypeConstants.FansclubType)
            {
                FansclubMessage fansclubMessage = JsonConvert.DeserializeObject<FansclubMessage>(str, deserializerSettings);
            }
            else if (actionType == ActionTypeConstants.GiftType)
            {
                GiftMessage giftMessage = JsonConvert.DeserializeObject<GiftMessage>(str, deserializerSettings);
            }
            else if (actionType == ActionTypeConstants.LikeType)
            {
                LikeMessage likeMessage = JsonConvert.DeserializeObject<LikeMessage>(str, deserializerSettings);
            }
            else if (actionType == ActionTypeConstants.MemberType)
            {
                MemberMessage memberMessage = JsonConvert.DeserializeObject<MemberMessage>(str, deserializerSettings);
            }
            else if (actionType == ActionTypeConstants.RoomType)
            {
                RoomMessage roomMessage = JsonConvert.DeserializeObject<RoomMessage>(str, deserializerSettings);
            }
            else if (actionType == ActionTypeConstants.RoomUserSeqType)
            {
                RoomUserSeqMessage roomUserSeqMessage = JsonConvert.DeserializeObject<RoomUserSeqMessage>(str, deserializerSettings);
            }
            else if (actionType == ActionTypeConstants.SocialType)
            {
                SocialMessage socialMessage = JsonConvert.DeserializeObject<SocialMessage>(str, deserializerSettings);
            }

            // ulong uid = Convert.ToUInt64(record.GetValueOrDefault("user_id"));
            // string nickname = (string)record.GetValueOrDefault("nickname");
            // string avatarUrl = (string)record.GetValueOrDefault("avatar");
            // ActorManager.Instance.AddActor(uid, nickname, avatarUrl);
        };

        channel.BasicQos(0, 1, false);
        consumerTag = channel.BasicConsume("application", false, consumer);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy");
        channel.BasicCancel(consumerTag);
        channel.Close();
    }
}
