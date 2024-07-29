using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    private float elapsedTime = 0f; // Elapsed time since the stopwatch started
    public TextMeshProUGUI stopwatchText; // Reference to the TextMeshPro text component
    public bool isRunning = false; // Flag to check if the stopwatch is running

    void Start()
    {
        elapsedTime = 0f;
    }

    void Update()
    {
        if (isRunning)
        {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Display the elapsed time on the TextMeshPro text component
            stopwatchText.text = FormatTime(elapsedTime);
        }
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }

    public void ResetStopwatch()
    {
        isRunning = false;
        elapsedTime = 0f;
        stopwatchText.text = FormatTime(elapsedTime);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}