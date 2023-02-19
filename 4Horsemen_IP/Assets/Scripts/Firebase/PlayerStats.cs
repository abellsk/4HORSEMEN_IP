using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerStats
{
    public string userName;
    public string totalTimeSpent;
    public string timeSpentRoom1;
    public string timeSpentRoom2;
    public string timeSpentRoom3;
    public int secondsSpentRoom1;
    public int secondsSpentRoom2;
    public int secondsSpentRoom3;
    public int totalSecondsSpent;
    public long updatedOn;
    public long createdOn;

    public PlayerStats()
    {

    }

    public PlayerStats(string userName, string totalTimeSpent, string room1, string room2, string room3,
        int sRoom1, int sRoom2, int sRoom3, int totalSecondsSpent = 0)
    {
        this.userName = userName;
        this.timeSpentRoom1= room1;
        this.timeSpentRoom2 = room2;
        this.timeSpentRoom3 = room3;
        this.secondsSpentRoom1 = sRoom1;
        this.secondsSpentRoom2 = sRoom2;
        this.secondsSpentRoom3 = sRoom3;

        this.totalTimeSpent = totalTimeSpent; 
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
