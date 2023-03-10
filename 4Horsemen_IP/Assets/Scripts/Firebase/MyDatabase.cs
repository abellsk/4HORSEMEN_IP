using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;
using Firebase.Extensions;
using System;
using JetBrains.Annotations;
using UnityEditor.Rendering;

public class MyDatabase : MonoBehaviour
{
    DatabaseReference dbref; //Database Reference
    DatabaseReference playerRef; //Player path reference
    DatabaseReference dbPlayerStatsReference;
    DatabaseReference dbLeaderboardsReference;


    // Start is called before the first frame update
    void Awake()
    {
        InitializeFirebase();
        //CreateNewPlayer(playerRef, "bruh", 888);

        //GetAllPlayers();

    }

    public void InitializeFirebase()
    {
        //intialise out database
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerstats");
        dbLeaderboardsReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");

        //player reference
        playerRef = FirebaseDatabase.DefaultInstance.GetReference("players");

    }
    //create a new entry if it is the first time playing and update if there already is one
    public void UpdatePlayerStats(string uuid, string time, string room1, string room2, string room3,
        int sRoom1, int sRoom2, int sRoom3, int stime, string userName)
    {
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        //read data and check if already have uuid entry
        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error creating your entries, ERROR: " + task.Exception);
            } else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
                if (playerStats.Exists)
                {
                    //creating a temp object sp which stores info from the player stats
                    PlayerStats sp = JsonUtility.FromJson<PlayerStats>(playerStats.GetRawJsonValue());
                    sp.secondsSpentRoom1 += sRoom1;
                    sp.secondsSpentRoom2 += sRoom2;
                    sp.secondsSpentRoom3 += sRoom3;
                    sp.timeSpentRoom1 += room1;
                    sp.timeSpentRoom2 += room2;
                    sp.timeSpentRoom3 += room3;
                    sp.totalSecondsSpent += stime;
                    //sp.updatedOn = sp.GetTimeUnix();
                    //sp.updatedOn = sp.GetTimeUnix();
                    
                    //check if new high score
                    if(sRoom1 > sp.secondsSpentRoom1)
                    {
                        sp.secondsSpentRoom1 = sRoom1;
                        sp.timeSpentRoom1 = room1;
                        sp.totalSecondsSpent = stime;
                        UpdatePlayerLeaderboardEntry(uuid, sp.secondsSpentRoom1, sp.secondsSpentRoom2, sp.secondsSpentRoom3, sp.totalSecondsSpent, sp.updatedOn);
                    }

                    else if(sRoom2 < sp.secondsSpentRoom2)
                    {
                        sp.secondsSpentRoom2 = sRoom2;
                        sp.timeSpentRoom2 = room2;
                        sp.totalSecondsSpent = stime;
                    }

                    else if(sRoom3 < sp.secondsSpentRoom3)
                    {
                        sp.secondsSpentRoom3 = sRoom3;
                        sp.timeSpentRoom3 = room3;
                        sp.totalSecondsSpent = stime;
                    }



                    //path: playerStats/$uuid
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.PlayerStatsToJson());
                }
                else
                {
                    PlayerStats sp = new PlayerStats(userName, time, room1, room2, room3, sRoom1, sRoom2, sRoom3, stime);
                    
                    PlayerStats lb = new PlayerStats(userName, time, room1, room2, room3, sRoom1, sRoom2, sRoom3);

                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.PlayerStatsToJson());
                    dbLeaderboardsReference.Child(uuid).SetRawJsonValueAsync(lb.PlayerStatsToJson());
                }
            }

        });

    }
    public void UpdatePlayerLeaderboardEntry(string uuid, int room1, int room2, int room3, int time, long updatedOn)
    {
        //path: leaderboards/$uuid/timeInRoom1
        //path: leaderboards/$uuid/updatedOn
        dbLeaderboardsReference.Child(uuid).Child("timeInRoom1").SetValueAsync(room1);
        dbLeaderboardsReference.Child(uuid).Child("timeInRoom2").SetValueAsync(room2);
        dbLeaderboardsReference.Child(uuid).Child("timeInRoom3").SetValueAsync(room3);
        dbLeaderboardsReference.Child(uuid).Child("totalTimeSpent").SetValueAsync(time);
        dbLeaderboardsReference.Child(uuid).Child("updatedOn").SetValueAsync(updatedOn);
    }

    public async Task<List<Leaderboard>> GetLeaderboard(int limit = 6)
    {
        Query q = dbLeaderboardsReference.OrderByChild("timeTaken").LimitToLast(limit);
        List<Leaderboard> leaderboardList = new List<Leaderboard>();
        await dbLeaderboardsReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error getting leaderboard entries, : ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;
                Debug.Log("ds...: " + ds.GetRawJsonValue());
                if (ds.Exists)
                {
                    int rankCounter = 1;
                    foreach (DataSnapshot d in ds.Children)
                    {
                        //creating temp objects based on the results
                        Leaderboard lb = JsonUtility.FromJson<Leaderboard>(d.GetRawJsonValue());

                        //adding items to the list
                        leaderboardList.Add(lb);

                        //Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Time Spent {2}", rankCounter, lb.userName, lb.totalTimeSpent);
                    }

                    //list from ascending to decending order
                    leaderboardList.Reverse();

                    //for each Leaderboard object inside our leaderboard list
                    foreach(Leaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Time Spent {2}", rankCounter, lb.userName, lb.totalTimeSpent);
                        rankCounter++;
                    }
                }
            }
        });
        return leaderboardList;
    }


    //retrieve data from authenticate
    public async Task<PlayerStats> GetPlayerStats(string uuid)
    {
        Query q = dbPlayerStatsReference.Child(uuid).LimitToFirst(1);
        PlayerStats playerStats = null;

        await dbPlayerStatsReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error retrieving player stats : ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;
                if (ds.Child(uuid).Exists)
                {
                    playerStats = JsonUtility.FromJson<PlayerStats>(ds.Child(uuid).GetRawJsonValue());

                    Debug.Log("ds... : " + ds.GetRawJsonValue());
                    Debug.Log("Player stats values... " + playerStats.PlayerStatsToJson());

                }
            }
        });
        return playerStats;
    }

    public void DeletePlayerStats(string uuid)
    {
        dbPlayerStatsReference.Child(uuid).RemoveValueAsync();
        dbLeaderboardsReference.Child(uuid).RemoveValueAsync();
    }


    /*
    private void CreateNewPlayer(DatabaseReference dbref, string playerName, int score)
    {
        //User p = new User(playerName, score);
        //var will just assume the datatype on the right 
        //gives me a path to write to
        var playerPath = dbref.Push();

        //playerPath.SetRawJsonValueAsync(JsonUtility.ToJson(p));

        //dbref.Push().SetRawJsonValueAsync(JsonUtility.ToJson(p));
    }

    public void UpdateInfo()
    {
        DatabaseReference dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        dbReference.Child("players").Child("-NFvmOvVSg8m1jPXDYea").Child("score").SetValueAsync("999");
    }

    private void GetAllPlayers()
    {
        playerRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log ("Issue with task...");
            }
            else if (task.IsCompleted)
            {
                //start receiving values and printout
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    foreach(DataSnapshot ds in snapshot.Children)
                    {
                        User p = JsonUtility.FromJson<User>(ds.GetRawJsonValue());
                        //Debug.LogFormat("Player data: {0}, level {1}", p.username, p.score);
                    }
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
