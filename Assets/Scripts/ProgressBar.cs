using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Mirror;

public class ProgressBar : NetworkBehaviour
{

    // Used online resources to get this code.
    // Source : 
    // https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#timer

    public bool progressing = false;

    // Sync Time Remaining
    [SyncVar] public float timeRemaining = 120;

    //public Text timeText;
    public Slider progressSlider;
    public Image barFill;

    public Text NavigationColor;
    public Text ReactorColor;
    public Texture gorilla2Tex;

    GameManager gm;
    NetworkManager nm;

    public GameObject monkey;
    public NavMeshAgent agentM;
    public GameObject gorilla;
    public NavMeshAgent agentG;

    public GorillaMovement gorill;
    //public NodeInstanceManager monk;

    public Gradient barGradient;

    public Vector3 sizeDelta;
    public GameObject spawnLoc;

    private static bool spawned = false;
    public bool teleport = false;
    private bool shouldTeleport = false;

    public float monkeySpawnTime = 4.3f;

    public GameObject wpArrow;
    UIManager uim;

    public AudioSource intruderAlert;
    public Animator anotherGorillaText;

    void Start()
    {
        progressing = true;
        gm = FindObjectOfType<GameManager>();
        nm = GameObject.FindObjectOfType<NetworkManager>();
        barFill.color = barGradient.Evaluate(1f);
        uim = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //-- for netcode --d// 
        
        if (!monkey) {
            monkey = GameObject.FindGameObjectWithTag("Monkey");
            agentM = monkey.GetComponent<NavMeshAgent>();
        }
        if (!gorilla) {
            gorilla = GameObject.FindGameObjectWithTag("Gorilla");
            agentG = gorilla.GetComponent<NavMeshAgent>();
            gorill = gorilla.GetComponent<GorillaMovement>();
        }
        

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
                if(timeRemaining < 60 && spawned == false)
                {
                    spawned = true;
                    GameObject gorilla2 = Instantiate( nm.spawnPrefabs[0] );
                    NetworkServer.Spawn(gorilla2);
                    intruderAlert.Play();
                    anotherGorillaText.Play("AnotherGorillaSlideIn");
                    /*GameObject mat = GameObject.Find("QuadDrawGorilla_LowPoly_UVUnwrapped_final1(Clone)");
                    Renderer render = mat.GetComponent<Renderer>();
                    render.material.SetTexture("Gorilla2Body", gorilla2Tex);*/

                }
                else if(timeRemaining < 90)
                {
                    agentM.speed = 40;
                    agentG.speed = 10;
                    
                    gorill._SPEED = 10;
                    gorill.chargeCooldown = 2f;
                    
                    NodeInstanceManager.monkCooldown = 1.5f;
                    //progressSlider.transform.localScale = Vector3.Lerp(progressSlider.transform.localScale, sizeDelta, 1f);
                }
            }
            else {
                // Set the time remaining to zero.
                timeRemaining = 0;
                if (!shouldTeleport)
                {
                    StartCoroutine("MonkeyTeleport");
                    shouldTeleport = true;
                }
                // Set progressing to false.
                progressing = false;
                //uim.ReplaceProgressBar();
                teleport = true;
                //gm.Victory();
            }
        }
    }

    IEnumerator MonkeyTeleport()
    {
        monkey.GetComponent<MonkeyMovement>().StopEnemy();
        yield return new WaitForSeconds(2f);
        monkey.GetComponent<MonkeyMovement>().TeleportOut();
    }
}
