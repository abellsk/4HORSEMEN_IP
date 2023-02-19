using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerStats
{
    public string userName;
    public string totalTimeSpent;
    public string timeSpentRoom1;
    public int timeSpentRoom2;
    public int timeSpentRoom3;
    public int secondsSpentRoom1;
    public int secondsSpentRoom2;
    public int secondsSpentRoom3;
    public int totalSecondsSpent;
    public long updatedOn;
    public long createdOn;

    public PlayerStats()
    {

    }

    public PlayerStats(string userName, string room1, int sRoom1, int room2, int room3, int totalSecondsSpent = 0)
    {
        this.userName = userName;
        this.timeSpentRoom1= room1;
        this.secondsSpentRoom1 = sRoom1;
        this.timeSpentRoom2 = room2;
        this.timeSpentRoom3 = room3;
        this.totalSecondsSpent = totalSecondsSpent;

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
