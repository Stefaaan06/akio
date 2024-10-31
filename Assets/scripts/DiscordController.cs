using System;
using Discord;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DiscordController : MonoBehaviour
{
    public string applicationIDFilePath = "Assets/applicationID.txt";
    private long startTime;
    private Stopwatch stopwatch;

    public Discord.Discord discord;
    private ActivityManager activityManager;
    public Activity activity;

    void Awake()
    {
        long applicationID = ReadApplicationIDFromFile(applicationIDFilePath);
        discord = new Discord.Discord(applicationID, (ulong)Discord.CreateFlags.NoRequireDiscord);
        
        activityManager = discord.GetActivityManager();
        
        startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    int sceneIndex = 0;
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneIndex = scene.buildIndex;
        
        if (sceneIndex > 1)
        {
            stopwatch = FindFirstObjectByType<Stopwatch>();
        }
        UpdateStatus();
    }

    void OnApplicationQuit()
    {
        try
        {
            if (this.enabled)
            {
                discord.Dispose();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"DiscordManager was unable to dispose of Discord Activity!\n{ex}", this);
        }
    }

    
    private float updateInterval = 0.5f; 
    private float nextUpdateTime = 0.0f;

    void Update()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch (Exception ex)
        {
            Debug.LogError($"DiscordManager lost callbacks!\n{ex}", this);
            this.enabled = false;
        }
    }
    
    void LateUpdate()
    {
        if(sceneIndex > 1 && stopwatch.isRunning)
        {
            UpdateStatus();
        }
    }
    void UpdateStatus()
    {
        string details = "";
        
        switch (sceneIndex)
        {
            case 0:
                details = "Login";
                break;
            case 1:
                details = "Main Menu";
                break;
            case > 1:
                details = "Ingame | " + stopwatch.FormatTime(stopwatch.elapsedTime);
                break;
        }
        try
        {
            activity.Details = details;
            activity = new Discord.Activity
            {
                Details = details,
                Assets = 
                {
                    LargeImage = "akiologo",
                    LargeText = "Download on itch.io for free"
                },
                Timestamps = 
                {
                    Start = startTime
                }
            };
            
            activityManager.UpdateActivity(activity, res =>
            {
                if (res != Discord.Result.Ok)
                {
                    Debug.LogWarning($"Failed updating Discord status! Error: {res}");
                }
            });
        }
        catch (Exception ex)
        {
            Debug.LogError($"DiscordManager lost connection!\n{ex}", this);
            this.enabled = false;
        }
    }

    long ReadApplicationIDFromFile(string filePath)
    {
        try
        {
            string idString = File.ReadAllText(filePath);
            return long.Parse(idString);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error reading applicationID from file: {ex.Message}", this);
            throw;
        }
    }
}