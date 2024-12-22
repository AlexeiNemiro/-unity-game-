using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
    private static string logFilePath = @"C:\Users\morze\Documents\GitHub\-unity-game-\My project (2)\log.txt";

    public static void Log(string message)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{System.DateTime.Now}: {message}");
        }
    }

    public static void LogWarning(string message)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{System.DateTime.Now}: WARNING: {message}");
        }
    }

    public static void LogError(string message)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{System.DateTime.Now}: ERROR: {message}");
        }
    }
}
