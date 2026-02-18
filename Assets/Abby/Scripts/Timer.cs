using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   
    [SerializeField] TMP_Text timerTextObject;
    [SerializeField] bool isFinished;
    float milliseconds, seconds, minutes;
    String timerString;
    void Start()
    {
        isFinished = false;
        milliseconds = 0;
        seconds = 0;
        minutes = 0;
        timerString = "0:00:00";
        timerTextObject.text = timerString;
    }

    void Update()
    {
        milliseconds += Time.deltaTime * 1000;
        milliseconds %= 100;
        seconds += Time.deltaTime;
        if(seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
        if (seconds < 10 && milliseconds < 10)
        {
            timerString = (int) minutes + ":0" + (int)seconds + ":0" + (int) milliseconds;
        }
        else if (seconds < 10)
        {
            timerString = (int) minutes + ":0" + (int)seconds + ":" + (int) milliseconds;
        }
        else if (milliseconds < 10)
        {
            timerString = (int) minutes + ":" + (int)seconds + ":0" + (int) milliseconds;
        }
       
        else
        {
            timerString = (int) minutes + ":" + (int)seconds + ":" + (int) milliseconds;
        }
  
        timerTextObject.text = timerString;
    }
}
