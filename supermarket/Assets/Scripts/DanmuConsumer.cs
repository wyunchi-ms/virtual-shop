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

public class DanmuConsumer : MonoBehaviour
{
    private IModel channel;
    private EventingBasicConsumer consumer;
    private string consumerTag;

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
            ulong uid = Convert.ToUInt64(record.GetValueOrDefault("user_id"));
            string nickname = (string)record.GetValueOrDefault("nickname");
            string avatarUrl = (string)record.GetValueOrDefault("avatar");
            ActorManager.Instance.AddActor(uid, nickname, avatarUrl);
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
