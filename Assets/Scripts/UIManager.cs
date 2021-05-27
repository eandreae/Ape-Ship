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

    [Header("Endgame")]
    public Animator[] minimapNodes;

    Animator destructTimer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SlideInUI", introDuration);
    }

    void SlideInUI()
    {
        blackBarTop.Play("TopBarLeave");
        blackBarBottom.Play("BottomBarLeave");
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
