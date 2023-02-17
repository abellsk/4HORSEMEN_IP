using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText1; //show current score in screen 1
    public TextMeshProUGUI playerScoreText2; //show current score in screen 2
    public TextMeshProUGUI playerScoreText3; //show current score in screen 3
    public TextMeshProUGUI playerScoreText4; //show current score in screen 4
    public static GameManager instance;

    //variables

    //displayname
    public TextMeshProUGUI userName;

    //pause menu
    public GameObject pauseMenu;
    private bool gamePaused;

    //gameover menu
    public GameObject gameOverMenu;

    //LogInMenus
    public GameObject logInPage;
    public GameObject signUpPage;
    public GameObject loggedInPage;
    public GameObject inputFields;

    //firebase
    public AuthManager auth;
    public MyDatabase firebaseMgr;
    public bool isPlayerStatUpdated;

    //timer
    //float currentTime = 0;
    public TextMeshProUGUI countdownText;


    void Awake()
    {
        //userName.text =  auth.GetCurrentUserDisplayName() + "!";

        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
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

    public void UpdatePlayerStat(int room1, int room2, int room3, int time)
    {
        firebaseMgr.UpdatePlayerStats(auth.GetCurrentUser().UserId, room1, room2, room3, time, auth.GetCurrentUserDisplayName());
    }

    public void ExistingAccount()
    {
        signUpPage.SetActive(false);
        logInPage.SetActive(true);
    }

    public void NoAccount()
    {
        logInPage.SetActive(false);
        signUpPage.SetActive(true);
    }
  
}
