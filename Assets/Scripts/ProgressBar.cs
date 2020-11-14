using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    // Used online resources to get this code.
    // Source : 
    // https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#timer

    public bool progressing = false;
    public float timeRemaining = 120;

    public Text timeText;

    void Start()
    {
        progressing = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the progressing is true.
        if ( progressing ){
            // Check if the time remaining isn't zero.
            if ( timeRemaining > 0 ){
                // Subtract the time by deltatime.
                timeRemaining -= Time.deltaTime;
                // Display the time remaining.
                displayTime(timeRemaining);
            }
            else {
                // Otherwise, the progress has finished.
                Debug.Log("Game Over! You survived!");
                // Set the time remaining to zero.
                timeRemaining = 0;
                // Set progressing to false.
                progressing = false;
            }
        }
    }

    void displayTime(float time) {

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
