using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BasketballController : MonoBehaviour {


    //displayname
    public TextMeshProUGUI displayName;

    //pause menu
    public GameObject pauseMenu;
    private bool gamePaused;

    //gameover menu
    public GameObject gameOverMenu;

    //firebase
    public AuthManager auth;
    public MyDatabase firebaseMgr;
    public bool isPlayerStatUpdated;
    public int xpPerGame = 5;

    //timer
    float currentTime = 0;
    public float startingTime;
    public TextMeshProUGUI countdownText;


    void Awake()
    {
        displayName.text = "Player: " + auth.GetCurrentUserDisplayName();
    }

    /*
    public void GameOver()
    {
        MoveSpeed = 0;
        gameOverMenu.SetActive(true);
        if (!isPlayerStatUpdated)
        {
            UpdatePlayerStat(scoreValue, xpPerGame, 30);
        }

    }

    public void Retry()
    {
        scoreValue = 0;
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    */

    public void UpdatePlayerStat(int score, int xp, int time)
    {
        firebaseMgr.UpdatePlayerStats(auth.GetCurrentUser().UserId, score, xp, time, auth.GetCurrentUserDisplayName());
    }

}
