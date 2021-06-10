using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UIManager : MonoBehaviour
{

    public Animator blackBarTop;
    public Animator blackBarBottom;
    public Animator progressBar;
    public Animator minimap;
    public Animator healthOxygen;
    public Animator roomName;
    public GameObject skipText;

    public float introDuration;
    public float teleportTime = 5f;
    public float blackBarAnimDuration = 1f;

    NetworkManager nm;
    Player1P p1p;
    public GameObject[] players;
    PlayerCamera pc;

    public GameObject monkey;
    public GameObject gorilla;

    public AudioSource intruderAlert;

    bool cutsceneOver;

    [Header("Endgame")]
    public AudioSource teleporterOffline;
    public GameObject[] minimapNodes;
    public Animator teleporterText;

   public GameObject batteryIndicator;


    // Start is called before the first frame update
    void Start()
    {
        nm = FindObjectOfType<NetworkManager>();
        pc = FindObjectOfType<PlayerCamera>();
        
        if(!nm){
            p1p = FindObjectOfType<Player1P>();
            pc.followPlayer = false;
            p1p.canMove = false;
        }
        else {
            pc.followPlayer = false;
        }
        
        Invoke("TeleportApes", teleportTime);
        Invoke("BlackBarsLeave", introDuration);
    }

    void Update()
    {
        if (!nm && Input.GetKeyDown(KeyCode.Return) && !cutsceneOver)
        {
            TeleportApes();
            BlackBarsLeave();
            SlideInUI();
            CancelInvoke();
            Destroy(skipText);
        }
    }

    void TeleportApes()
    {
        intruderAlert.Play();
        monkey.SetActive(true);
        gorilla.SetActive(true);
    }

    void BlackBarsLeave()
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        
        pc.followPlayer = true;

        if(!nm)
            p1p.canMove = true;
        else{
            foreach (GameObject p in players)
                p.GetComponent<Player>().canMove = true;
            progressBar = FindObjectOfType<ProgressBar>().GetComponent<Animator>();
        }

        blackBarTop.Play("TopBarLeave");
        blackBarBottom.Play("BottomBarLeave");
        Invoke("SlideInUI", blackBarAnimDuration);
        cutsceneOver = true;
    }

    void SlideInUI()
    {
        progressBar.Play("ProgressBarSlideIn");
        minimap.Play("MinimapSlideIn");
        healthOxygen.Play("HealthOxygenSlideIn");
        roomName.Play("RoomNameSlideIn");
    }

    public void ReplaceProgressBar()
    //Endgame stuff happens here
    {
        Debug.Log("We're in the endgame now!");
        progressBar.Play("ProgressBarFadeOut");
        //destructTimer.Play("TimerFadeIn");
        foreach (GameObject minimapNode in minimapNodes)
        {
            minimapNode.SetActive(false);
        }
        teleporterText.Play("GetToTeleporterSlideIn");
        batteryIndicator.SetActive(true);
    }

}
