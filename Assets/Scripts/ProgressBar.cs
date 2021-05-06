using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ProgressBar : MonoBehaviour
{

    // Used online resources to get this code.
    // Source : 
    // https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#timer

    public bool progressing = false;
    public float timeRemaining = 120;

    //public Text timeText;
    public Slider progressSlider;
    public Image barFill;

    public Text NavigationColor;
    public Text ReactorColor;

    GameManager gm;

    GameObject monkey;
    NavMeshAgent agentM;
    GameObject gorilla;
    NavMeshAgent agentG;

    public GorillaMovement gorill;
    public NodeInstanceManager monk;

    public Gradient barGradient;

    public Vector3 sizeDelta;


    void Start()
    {
        progressing = true;
        gm = FindObjectOfType<GameManager>();

        monkey = GameObject.FindGameObjectWithTag("Monkey");
        agentM = monkey.GetComponent<NavMeshAgent>();
        gorilla = GameObject.FindGameObjectWithTag("Gorilla");
        agentG = gorilla.GetComponent<NavMeshAgent>();
        barFill.color = barGradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {

        // Check if either NavigationColor or ReactorColor are red.
        if ( NavigationColor.text == "red" || ReactorColor.text == "red" ){
            // If either of them are red, set progressing to false.
            progressing = false;
        }
        else {
            // Set progressing to true.
            progressing = true;
        }
        
        // Check if the progressing is true.
        if ( progressing ){
            // Check if the time remaining isn't zero.
            if ( timeRemaining > 0 ){
                // Subtract the time by deltatime.
                timeRemaining -= Time.deltaTime;
                progressSlider.value = 120 - timeRemaining;
                barFill.color = barGradient.Evaluate(progressSlider.normalizedValue);
                if(timeRemaining < 60)
                {
                    agentM.speed = 40;
                    agentG.speed = 10;
                    gorill._SPEED = 10;
                    gorill.chargeCooldown = 2f;
                    monk.monkCooldown = 1.5f;
                }
                if (timeRemaining < 30 && (transform.localScale.x < 2) && (transform.localScale.y < 2) && (transform.localScale.z < 2))
                {
                    progressSlider.transform.localScale += sizeDelta;
                }
            }
            else {
                // Set the time remaining to zero.
                timeRemaining = 0;
                // Set progressing to false.
                progressing = false;
                gm.Victory();
            }
        }
    }
}
