using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;

    private float timer = 0.0f;
    private bool isTimer = true;

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            DisplayTime();
        }
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void stopTimer()
    {
        timer = 0.0f;
    }
}
