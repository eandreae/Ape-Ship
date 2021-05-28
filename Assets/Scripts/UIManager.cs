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

    public GameObject monkeyPrefab;
    public GameObject gorillaPrefab;

    public Transform monkeyDefaultPos;
    public Transform gorillaDefaultPos;

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
        Invoke("TeleportApes", teleportTime);
        Invoke("BlackBarsLeave", introDuration);
    }

    void TeleportApes()
    {
        Instantiate(monkeyPrefab, monkeyDefaultPos.position, Quaternion.identity);
        Instantiate(gorillaPrefab, gorillaDefaultPos.position, Quaternion.identity);
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
    {
        progressBar.Play("ProgressBarFadeOut");
        destructTimer.Play("TimerFadeIn");
        foreach (Animator minimapNode in minimapNodes)
        {
            minimapNode.Play("MinimapNodeFadeOut");
        }
    }

}
