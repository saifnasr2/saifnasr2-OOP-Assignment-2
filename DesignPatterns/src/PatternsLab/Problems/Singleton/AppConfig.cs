using System;

namespace PatternsLab.Problems.Singleton;

public class AppConfig
{
    private static readonly object _lock= new();
    
    public static int LoadCount;
    public string DbConnection { get; set; }
    public string Theme { get; set; }

    private AppConfig()
    {
        LoadCount++;
        Console.WriteLine($"[AppConfig] Loading settings from disk... (load #{LoadCount})");
        Thread.Sleep(300);
        DbConnection = "Server=localhost;Db=School";
        Theme = "Light";
    }

    public static AppConfig Config
    {
        get
        {
            lock (_lock)
            {
                if (field == null)
                {
                    field = new AppConfig();
                }
                return field;
            }
        }
    }

}

public class DatabaseService
{
    public AppConfig Config { get; } = AppConfig.Config;

    public void Connect() => Console.WriteLine($"Connecting to {Config.DbConnection}");
}

public class UiService
{
    public AppConfig Config { get; } = AppConfig.Config;

    public void Render() => Console.WriteLine($"UI is using theme: {Config.Theme}");
}