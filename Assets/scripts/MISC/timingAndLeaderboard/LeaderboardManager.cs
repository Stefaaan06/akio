using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine;
using Unity.Services.Core;

public class LeaderboardManager : MonoBehaviour
{
    public string leaderboardID = "time";
    public TextMeshProUGUI leaderboardText;
    public RectTransform content;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI rankText;

    [Serializable]
    public class LeaderboardEntry
    {
        public string playerId;
        public string playerName;
        public int rank;
        public float score;
    }

    [Serializable]
    public class LeaderboardData
    {
        public int limit;
        public int total;
        public List<LeaderboardEntry> results;
    }

    public async void ReportScore(double score)
    {
        await UnityServices.InitializeAsync();
        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, score);
    }

    public void DisplayUserName()
    {
        playerNameText.text = $"Player: {AuthenticationService.Instance.PlayerName}";
    }

    public async void DisplayPersonalBest()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardID);
        rankText.text = $"Rank: {scoresResponse.Rank + 1}";
    }

    public async void GetScores()
    {
        try
        {
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID, new GetScoresOptions { Limit = 100 });
            string json = JsonConvert.SerializeObject(scoresResponse);
            LeaderboardData leaderboardData = JsonConvert.DeserializeObject<LeaderboardData>(json);

            leaderboardText.text = ""; 

            foreach (var entry in leaderboardData.results)
            {
                int rank = entry.rank + 1;
                string playerName = entry.playerName;
                float score = entry.score;

                leaderboardText.text += $"{rank}\t{playerName}\t\t{score:F2}\n";
            }

            float requiredHeight = leaderboardData.results.Count * leaderboardText.fontSize * 0.9f; 
            content.sizeDelta = new Vector2(content.sizeDelta.x, requiredHeight);

        }
        catch (Exception ex)
        {
            Debug.LogError($"Error fetching leaderboard scores: {ex.Message}");
        }
    }
}