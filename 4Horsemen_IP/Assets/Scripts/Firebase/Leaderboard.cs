using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Leaderboard
{
    public string userName;
    public int totalTimeSpent;
    public long updatedOn;

    public Leaderboard()
    {

    }

    public Leaderboard(string userName, int totalTimeSpent)
    {
        this.userName = userName;
        this.totalTimeSpent = totalTimeSpent;
        this.updatedOn = GetTimeUnix();
    }   

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public string LeaderboardToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
