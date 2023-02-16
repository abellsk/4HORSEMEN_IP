using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using JetBrains.Annotations;

public class PlayerStatsManager : MonoBehaviour
{
    public TextMeshProUGUI timeInRoom1;
    public TextMeshProUGUI timeInRoom2;
    public TextMeshProUGUI timeInRoom3;
    public TextMeshProUGUI playerTimeSpent;
    public TextMeshProUGUI playerLastPlayed;
    public TextMeshProUGUI playerName;

    public MyDatabase fbMgr;
    public AuthManager auth;

    // Start is called before the first frame update
    void Start()
    {

        ResetStatsUI();
        //retrieve current logged in user's uuid
        //update UI
        UpdatePlayerStats(auth.GetCurrentUser().UserId);
    }

    public async void UpdatePlayerStats(string uuid)
    {
        PlayerStats playerStats = await fbMgr.GetPlayerStats(uuid);

        if(playerStats != null)
        {

            Debug.Log("playerstats... : " + playerStats.PlayerStatsToJson());
            
            timeInRoom1.text = playerStats.timeSpentRoom1 + "secs";
            timeInRoom2.text = playerStats.timeSpentRoom2 + "secs";
            timeInRoom3.text = playerStats.timeSpentRoom3 + "secs";
            playerTimeSpent.text = playerStats.totalTimeSpent + "secs";
            playerLastPlayed.text = UnixToDateTime(playerStats.updatedOn);

        }
        else
        {
            ResetStatsUI();
        }

        playerName.text = auth.GetCurrentUserDisplayName();
    }

    public void ResetStatsUI()
    {
        timeInRoom1.text = "0";
        timeInRoom2.text = "0";
        timeInRoom3.text = "0";
        playerTimeSpent.text = "0";
        playerLastPlayed.text = "0";
    }

    public void DeletePlayerStats()
    {
        fbMgr.DeletePlayerStats(auth.GetCurrentUser().UserId);

        UpdatePlayerStats(auth.GetCurrentUser().UserId);
    }

    public string UnixToDateTime(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMM yyyy");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }
   
}
