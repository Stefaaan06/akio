using System.IO;
using System.Text;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private string filePath;
    public TMPro.TMP_Text leaderboardText;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/leaderboard.dat";
    }

    public void AddEntry(string playerName, string time)
    {
        LeaderboardData leaderboard = LoadData();
        LeaderboardEntry newEntry = new LeaderboardEntry { playerName = playerName, time = time };
        leaderboard.entries.Add(newEntry);
        SaveData(leaderboard);
    }

    private void SaveData(LeaderboardData data)
    {
        string json = JsonUtility.ToJson(data);
        string encryptedData = EncryptionUtility.Encrypt(json);
        File.WriteAllText(filePath, encryptedData);
    }

    private LeaderboardData LoadData()
    {
        if (!File.Exists(filePath))
        {
            return new LeaderboardData();
        }

        string encryptedData = File.ReadAllText(filePath);
        string json = EncryptionUtility.Decrypt(encryptedData);
        return JsonUtility.FromJson<LeaderboardData>(json);
    }

    public void DisplayLeaderboard()
    {
        LeaderboardData leaderboard = LoadData();
        leaderboard.entries.Sort((a, b) => string.Compare(a.time, b.time)); // Assuming time is formatted correctly

        StringBuilder sb = new StringBuilder();
        foreach (var entry in leaderboard.entries)
        {
            sb.AppendLine($"{entry.playerName}: {entry.time}");
        }

        leaderboardText.text = sb.ToString();
    }
    
    public void ResetLeaderboard()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        DisplayLeaderboard();
    }
}