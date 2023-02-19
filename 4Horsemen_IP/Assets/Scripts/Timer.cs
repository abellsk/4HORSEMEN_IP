using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI textTimer;
    private float timer = 0.0f;
    private bool isTimer = true;
    private int minutes;
    private int seconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            StartTimer();
        }
    }

    public void StartTimer()
    {
        minutes = Mathf.FloorToInt(timer / 60.0f);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }
}
