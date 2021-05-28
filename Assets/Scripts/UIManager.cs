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
    public float blackBarAnimDuration = 1f;

    Player1P p1p;
    PlayerCamera pc;
    MonkeyMovement[] mms;
    GorillaMovement[] gms;

    [Header("Endgame")]
    public Animator[] minimapNodes;

    Animator destructTimer;

    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerCamera>();
        p1p = FindObjectOfType<Player1P>();
        pc.followPlayer = false;
        p1p.canMove = false;
        Invoke("BlackBarsLeave", introDuration);

        foreach (MonkeyMovement mm in mms)
        {
            mm.StopMonkey();
        }

        foreach (GorillaMovement gm in gms)
        {
            gm._SPEED = 0f;
        }
    }

    void BlackBarsLeave()
    {
        pc.followPlayer = true;
        p1p.canMove = true;
        blackBarTop.Play("TopBarLeave");
        blackBarBottom.Play("BottomBarLeave");
        Invoke("SlideInUI", blackBarAnimDuration);

        foreach (MonkeyMovement mm in mms)
        {
            mm.ResumeMonkey();
        }

        foreach (GorillaMovement gm in gms)
        {
            gm._SPEED = 6f;
        }
    }

    void SlideInUI()
    {
        progressBar.Play("ProgressBarSlideIn");
        minimap.Play("MinimapSlideIn");
        healthOxygen.Play("HealthOxygenSlideIn");
        roomName.Play("RoomNameSlideIn");
    }

    public void ReplaceProgressBar()
    {
        progressBar.Play("ProgressBarFadeOut");
        destructTimer.Play("TimerFadeIn");
        foreach (Animator minimapNode in minimapNodes)
        {
            minimapNode.Play("MinimapNodeFadeOut");
        }
    }

}
