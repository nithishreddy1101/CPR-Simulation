using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private bool timerIsRunning = true; // A flag to control the timer's active state

    // Consider using a public method to start/reset the timer if needed
    // void Start()
    // {
    //     timerIsRunning = true;
    // }

    void Update()
    {
        if (timerIsRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                if (remainingTime < 0)
                {
                    remainingTime = 0;
                }
            }
            else // Timer has run out
            {
                remainingTime = 0; // Ensure it stays at zero
                timerIsRunning = false; // Stop the timer
                Debug.Log("Timer finished!"); // Optional: Log when the timer is done
                // You can add other game logic here for when the timer ends
                // For example: EndGame(), ShowResults(), etc.
            }

            // Format and display the time
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Optional: A public method to reset or start the timer from other scripts
    public void StartTimer(float duration)
    {
        remainingTime = duration;
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }
}