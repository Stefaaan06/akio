using System;
using System.Collections.Generic;

[Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public string time; // Format: 00:00:000 (MIN:SEC:MILSEC)
}

[Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}