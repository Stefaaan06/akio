using System;
using UnityEngine;

public class finish : MonoBehaviour
{
    public Stopwatch time;
    public LeaderboardManager leaderboardManager;
    public PlayerUIManager playerUIManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DateTime now = DateTime.Now;
            playerUIManager.death();
            time.StopStopwatch();

            // Convert elapsed time to a score (e.g., in milliseconds)
            double score = (double)(time.elapsedTime);
            leaderboardManager.ReportScore(score);
        }
    }
}