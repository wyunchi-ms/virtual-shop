using System;
using System.Collections.Generic;
using System.Text;

using Serilog;

using UnityEngine;

public abstract class BaseLogger
{
    public abstract string LogPrefix { get; }

    public void Debug(string message)
    {
        float now = Time.time;
        Log.Debug(string.Format("[{0}:{1}] {2}", now, LogPrefix, message));
    }

    public void Debug(string messageTemplate, params object[] propertyValues)
    {
        float now = Time.time;
        Log.Debug(string.Format("[{0}:{1}] {2}", now, LogPrefix, messageTemplate), propertyValues);
    }

    public void Information(string message)
    {
        float now = Time.time;
        Log.Information(string.Format("[{0}:{1}] {2}", now, LogPrefix, message));
    }

    public void Information(string messageTemplate, params object[] propertyValues)
    {
        float now = Time.time;
        Log.Information(string.Format("[{0}:{1}] {2}", now, LogPrefix, messageTemplate), propertyValues);
    }

    public void Warning(string message)
    {
        float now = Time.time;
        Log.Warning(string.Format("[{0}:{1}] {2}", now, LogPrefix, message));
    }

    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        float now = Time.time;
        Log.Warning(string.Format("[{0}:{1}] {2}", now, LogPrefix, messageTemplate), propertyValues);
    }

    public void Error(string message)
    {
        float now = Time.time;
        Log.Error(string.Format("[{0}:{1}] {2}", now, LogPrefix, message));
    }

    public void Error(string messageTemplate, params object[] propertyValues)
    {
        float now = Time.time;
        Log.Error(string.Format("[{0}:{1}] {2}", now, LogPrefix, messageTemplate), propertyValues);
    }

    public void Error(Exception exception, string message)
    {
        float now = Time.time;
        Log.Error(exception, string.Format("[{0}:{1}] {2}", now, LogPrefix, message));
    }

    public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        float now = Time.time;
        Log.Error(exception, string.Format("[{0}:{1}] {2}", now, LogPrefix, messageTemplate), propertyValues);
    }
}