using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NumberPad : MonoBehaviour
{
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

    [SerializeField] private AudioSource buttonBeep;

    public Animator CameraFall;
    public Animator Doors;
    public Animator Doors2;
    public GameObject checkeredFloor;


    public void CheckPassword()
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
        if (inputPasswordList.Count >= 4)
        {
            CheckPassword();
        }
        UpdateDisplay();
    }

    private void correctPasswordGiven()
    {
        if (allowMultipleActivations)
        {
            onCorrectPassword.Invoke();
            StartCoroutine(ResetKeycode());
        }
        else if (!allowMultipleActivations && !hasUsedCorrectCode)
        {
            onCorrectPassword.Invoke();
            hasUsedCorrectCode = true;
            codeDisplay.text = successText;
            StartCoroutine(DoorsOpen());
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
        if (inputPasswordList.Count <= 0)
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

    IEnumerator DoorsOpen()
    {
        Doors.Play("DoorSwinging");
        Doors2.Play("DoorSwinging2");
        yield return new WaitForSeconds(1.5f);
        checkeredFloor.SetActive(false);
        CameraFall.Play("Falling");
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(resetTime);

        GameManager.instance.NextLevel();
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
