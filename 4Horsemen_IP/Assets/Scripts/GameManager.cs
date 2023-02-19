using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

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

    //numberpad
    public List<int> correctPassword = new List<int>();
    private List<int> inputPasswordList = new List<int>();
    public TMP_InputField incorrectText;
    [SerializeField] public TextMeshProUGUI codeDisplay;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private string successText;
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;
    private bool hasUsedCorrectCode = false;
    public bool allowMultipleActivations = false;
    public bool HasUsedCorrectCode { get { return hasUsedCorrectCode; } }
    [Header("Buttons")]
    public Animator Button1;
    public Animator Button2;
    public Animator Button3;
    public Animator Button4;
    public Animator Button5;
    public Animator Button6;
    public Animator Button7;
    public Animator Button8;
    public Animator Button9;

    public Animator CameraFall;

    //timer
    [Header("others")]
    public TextMeshProUGUI textTimerRoom1;
    public TextMeshProUGUI textTimerRoom2;
    public TextMeshProUGUI textTimerRoom3;
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

    //room2Interacables
    public GameObject roomTwoDoor;
    public GameObject checkeredFloor;

    [SerializeField] private AudioSource buttonBeep;


    void Awake()
    {
        StartTimer();
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
        //UpdatePlayerStat(timeInRoom1, seconds, secondsInRoom2, secondsInRoom3, totalSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            StartTimer();
        }
        checkScore();
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
        isTimer = true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        ResetTimer();
        UpdatePlayerStat(totalTime, timeInRoom1, timeInRoom2, timeInRoom3, secondsInRoom1, secondsInRoom2, secondsInRoom3, totalSeconds);
    }

    public void UpdatePlayerStat(string time, string room1, string room2, string room3, int sRoom1, int sRoom2, int sRoom3, int stime)
    {
        firebaseMgr.UpdatePlayerStats(auth.GetCurrentUser().UserId, time, room1, room2, room3, sRoom1, sRoom2, sRoom3, stime, auth.GetCurrentUserDisplayName());
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

    public void StartTimer()
    {
        minutes = Mathf.FloorToInt(timer / 60.0f);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimerRoom1.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        textTimerRoom2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //textTimerRoom3.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        secondsInRoom1 = Mathf.RoundToInt(timer);
        secondsInRoom2 = Mathf.RoundToInt(timer);
        secondsInRoom3 = Mathf.RoundToInt(timer);


    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }


    public void shootScore()
    {
        targetScore++;
        Debug.Log(targetScore);
        playerScoreText1.text = targetScore.ToString();
        playerScoreText2.text = targetScore.ToString();
        playerScoreText3.text = targetScore.ToString();
        playerScoreText4.text = targetScore.ToString();
    }

    public void checkScore()
    {
        if (targetScore == 3)
        {
            roomTwoDoor.SetActive(false);
            checkeredFloor.SetActive(false);
        }
    }

    public void UserNumberEntry(int selectedNum)
    {
        if (inputPasswordList.Count >= 4)
        {
            return;
        }

        else
        {
            inputPasswordList.Add(selectedNum);
            if (selectedNum == 1)
            {
                Button1.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 2)
            {
                Button2.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 3)
            {
                Button3.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 4)
            {
                Button4.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 5)
            {
                Button5.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 6)
            {
                Button6.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 7)
            {
                Button7.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 8)
            {
                Button8.Play("ButtonPressed");
                buttonBeep.Play();
            }
            else if (selectedNum == 9)
            {
                Button9.Play("ButtonPressed");
                buttonBeep.Play();
            }
        }
        if(inputPasswordList.Count >= 4) 
        {
            CheckPassword();
        }
        UpdateDisplay();

    }

    private void CheckPassword()
    {
        for (int i = 0; i < correctPassword.Count; i++)
        {
            if (inputPasswordList[i] != correctPassword[i]) 
            {
                IncorrectPassword();
                return;
            }
        }
        correctPasswordGiven();
    }

    private void correctPasswordGiven()
    {
        if (allowMultipleActivations)
        {
            onCorrectPassword.Invoke();
            StartCoroutine(ResetKeycode());
        }
        else if(!allowMultipleActivations && !hasUsedCorrectCode)
        {
            onCorrectPassword.Invoke();
            hasUsedCorrectCode = true;
            codeDisplay.text = successText;

            checkeredFloor.gameObject.SetActive(false);
            CameraFall.Play("Falling");
            StartCoroutine(NextScene());
        }
    }


    private void IncorrectPassword()
    {
        codeDisplay.gameObject.SetActive(false);
        incorrectText.gameObject.SetActive(true);
        onIncorrectPassword.Invoke();
        StartCoroutine(ResetKeycode());
    }

    private void UpdateDisplay()
    {
        codeDisplay.text = null;
        for (int i = 0; i < inputPasswordList.Count; i++)
        {
            codeDisplay.text += inputPasswordList[i];
        }

    }

    public void DeleteEntry()
    {
        if(inputPasswordList.Count <= 0) 
        { 
            return;
        }
        else
        {
            var listposition = inputPasswordList.Count - 1;
            inputPasswordList.RemoveAt(listposition);
        }

        UpdateDisplay();
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(resetTime);

        NextLevel();
    }

    IEnumerator ResetKeycode() 
    {
        yield return new WaitForSeconds(resetTime);

        inputPasswordList.Clear();
        incorrectText.gameObject.SetActive(false);
        codeDisplay.gameObject.SetActive(true);
        codeDisplay.text = "0000";
    }

}
