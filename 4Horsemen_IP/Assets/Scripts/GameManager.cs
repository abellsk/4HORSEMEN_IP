using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText1; //show current score in screen 1
    public TextMeshProUGUI playerScoreText2; //show current score in screen 2
    public TextMeshProUGUI playerScoreText3; //show current score in screen 3
    public TextMeshProUGUI playerScoreText4; //show current score in screen 4
    public static GameManager instance;

    int targetScore = 0;

    //variables
    private int totalSeconds;
    private int secondsInRoom1;
    private int secondsInRoom2;
    private int secondsInRoom3;
    private string timeInRoom1;
    private string timeInRoom2;
    private string timeInRoom3;
    private string totalTime;
    private int minutes;
    private int seconds;

    //timer
    public TextMeshProUGUI textTimer;
    private float timer = 0.0f;
    private bool isTimer = true;


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
    private bool isPlayerStatsUpdated;

    //timer
    //float currentTime = 0;
    public TextMeshProUGUI countdownText;


    void Awake()
    {

        DisplayTime();
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

    private void Start()
    {
        UpdatePlayerStat(timeInRoom1, secondsInRoom1, secondsInRoom2, secondsInRoom3, totalSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            DisplayTime();
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

    public void StartGame()
    {
        isPlayerStatsUpdated = false;

    }

    public void NextLevel()
    {

    }

    public void UpdatePlayerStat(string room1, int sRoom1, int sRoom2, int sRoom3, int time)
    {
        firebaseMgr.UpdatePlayerStats(auth.GetCurrentUser().UserId, room1, sRoom1, sRoom2, sRoom3, time, auth.GetCurrentUserDisplayName());
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

    public void DisplayTime()
    {
        minutes = Mathf.FloorToInt(timer / 60.0f);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void stopTimer()
    {
        timer = 0.0f;
    }

    public void shootScore()
    {
        targetScore++;
        playerScoreText1.text = targetScore.ToString();
        playerScoreText2.text = targetScore.ToString();
        playerScoreText3.text = targetScore.ToString();
        playerScoreText4.text = targetScore.ToString();
    }

}
