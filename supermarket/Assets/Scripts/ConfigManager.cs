using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;
using UnityEngine;
using System.IO;

public class ConfigManager : MonoBehaviour
{
    public GameConfig gameConfig;
    
    void Start()
    {
        var deserializer = new DeserializerBuilder()
            .Build();
        string content = File.ReadAllText(@"E:\project\virtual-shop\supermarket\Configs\Game.yml");
        gameConfig = deserializer.Deserialize<GameConfig>(content);
    }
}
