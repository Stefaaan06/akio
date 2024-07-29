using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class finish : MonoBehaviour
{
    public Stopwatch time;
    public LeaderboardManager leaderboardManager;
    public PlayerUIManager playerUIManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd/MM");
            playerUIManager.death();
            time.StopStopwatch();
            leaderboardManager.AddEntry(formattedDate, time.FormatTime(time.elapsedTime));
        }
    }
}
