using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator blackBarTop;
    public Animator blackBarBottom;
    public Animator progressBar;
    public Animator minimap;
    public Animator healthOxygen;
    public Animator roomName;

    public float introDuration;
    public float teleportTime = 5f;
    public float blackBarAnimDuration = 1f;

    Player1P p1p;
    PlayerCamera pc;

    public GameObject monkey;
    public GameObject gorilla;

    [Header("Endgame")]
    public Animator[] minimapNodes;
    public Animator teleporterText;

    Animator destructTimer;

   public GameObject batteryIndicator;

    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerCamera>();
        p1p = FindObjectOfType<Player1P>();
        pc.followPlayer = false;
        p1p.canMove = false;
        Invoke("TeleportApes", teleportTime);
        Invoke("BlackBarsLeave", introDuration);
    }

    void TeleportApes()
    {
        monkey.SetActive(true);
        gorilla.SetActive(true);
    }

    void BlackBarsLeave()
    {
        pc.followPlayer = true;
        p1p.canMove = true;
        blackBarTop.Play("TopBarLeave");
        blackBarBottom.Play("BottomBarLeave");
        Invoke("SlideInUI", blackBarAnimDuration);
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
        progressBar.Play("ProgressBarFadeOut");
        //destructTimer.Play("TimerFadeIn");
        foreach (Animator minimapNode in minimapNodes)
        {
            minimapNode.Play("MinimapNodeFadeOut");
        }
        teleporterText.Play("GetToTeleporterSlideIn");
        batteryIndicator.SetActive(true);
    }

}
