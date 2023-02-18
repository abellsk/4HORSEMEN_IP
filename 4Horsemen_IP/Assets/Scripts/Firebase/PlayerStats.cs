using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerStats
{
    public string userName;
    public int totalTimeSpent;
    public int timeSpentRoom1;
    public int timeSpentRoom2;
    public int timeSpentRoom3;
    public long updatedOn;
    public long createdOn;

    public PlayerStats()
    {

    }

    public PlayerStats(string userName, int room1, int room2, int room3, int totalTimeSpent = 0)
    {
        this.userName = userName;
        this.timeSpentRoom1 = room1;
        this.timeSpentRoom2 = room2;
        this.timeSpentRoom3 = room3;
        this.totalTimeSpent = totalTimeSpent;

        var timestamp = this.GetTimeUnix();
        this.updatedOn = timestamp;
        this.createdOn = timestamp;
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }


    public string PlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
