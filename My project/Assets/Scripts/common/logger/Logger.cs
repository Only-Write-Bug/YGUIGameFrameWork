using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using common.logger;
using UnityEngine;
using util;

public class Logger
{
    private static string _defaultLogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "In_Game\\Log");
    private static string _defaultLogFileType = ".log";

    private static StringBuilder _curLogContent = new StringBuilder();
    
    public static void Log(string message, ELogLevel logLevel = ELogLevel.DEBUG)
    {
        Debug.Log($"Log :: {logLevel} :: {message}");
        _curLogContent.AppendLine($"Log :: {logLevel} :: {message}");
    }

    public static void Warn(string message, ELogLevel logLevel = ELogLevel.DEBUG)
    {
        Debug.LogWarning($"Warn :: {logLevel} :: {message}");
        _curLogContent.AppendLine($"Warn :: {logLevel} :: {message}");
    }

    public static void Error(string message, ELogLevel logLevel = ELogLevel.DEBUG)
    {
        Debug.LogError($"Error :: {logLevel} :: {message}");
        _curLogContent.AppendLine($"Error :: {logLevel} :: {message}");
    }

    public static void ResetLogFile()
    {
        Save();
        _curLogContent.Clear();
    }

    public static void Save()
    {
        IOUtil.CheckPath(_defaultLogFilePath);

        var curTimeStamp = DateTimeUtil.GetTimestampByUtcTime(DateTime.UtcNow);
        var saveLogFileName = curTimeStamp + _defaultLogFileType;

        _curLogContent = _curLogContent.Insert(0, $"UtcTime:{DateTime.UtcNow}\n");

        var logFilePath = Path.Combine(_defaultLogFilePath, saveLogFileName);
        File.WriteAllText(logFilePath, _curLogContent.ToString());
        _curLogContent.Clear();
    }
}
