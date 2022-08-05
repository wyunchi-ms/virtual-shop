using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Serilog;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.ffff} [{Level:u3}] {Message}{NewLine}{Exception}")
            .WriteTo.File("Logs\\.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
