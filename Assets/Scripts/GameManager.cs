using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public Text causeOfDeath;
    public GameObject pausePanel;
    public GameObject pauseMenuPanel;

    public AudioSource backgroundMusic;

    public Animator transitionPanel;

    public float returnDelay = 1f;
    public float deathAnimDuration = 1f;

    bool won = false;
    bool lost = false;

    Player1P p1p;

    void Start()
    {
        Time.timeScale = 1f;
        p1p = FindObjectOfType<Player1P>();
    }

    public void Victory()
    {
        if (!lost)
        {
            //Enabling a victory panel
            victoryPanel.SetActive(true);
            //Turning off the gameplay music
            backgroundMusic.volume = 0f;
            p1p.canMove = false;
            //We need to destroy the pause menu panel so the player can't pause once the game is technically over
            Destroy(pausePanel);
            won = true;
        }
    }

    public void Defeat(int cause)
    {
        if (!won)
        {
            //Enabling a defeat panel
            Invoke("DefeatPanel", deathAnimDuration);
            backgroundMusic.volume = 0f;
            Destroy(pausePanel);
            lost = true;
            p1p.canMove = false;
            p1p.GetComponent<Animator>().Play("PlayerDeath");

            if (cause == 1)
            {
                causeOfDeath.text = "Cause of Death: Loss of Oxygen";
            }
            else if (cause == 2)
            {
                causeOfDeath.text = "Cause of Death: Beaten to Death by Gorilla";
            }
            else if (cause == 3)
            {
                causeOfDeath.text = "Cause of Death: Obliterated by Self-Destruct Sequence";
            }
            else if (cause == 4)
            {
                causeOfDeath.text = "Cause of Death: Entering the...void?";
            }
        }
    }

    void DefeatPanel()
    {
        defeatPanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        transitionPanel.Play("PanelOutro");
        Invoke("FinallyReturnToMenu", returnDelay);
    }

    void FinallyReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
