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
    int milliseconds, seconds, minutes;
    String timerString;
    void Start()
    {
        milliseconds = 0;
        seconds = 0;
        minutes = 0;
        timerString = "00:00:00";
        timerTextObject.text = timerString;
    }

    // Update is called once per frame
    void Update()
    {
        milliseconds += (int) (Time.deltaTime * 1000);
        milliseconds %= 60;
        seconds += (int) Time.deltaTime;
        minutes += seconds / 60;
        seconds %= 60;
        timerString = minutes + ":" + seconds + ":" + milliseconds;
        timerTextObject.text = timerString;
    }
}
