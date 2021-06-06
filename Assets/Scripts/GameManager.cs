using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel1P;
    public GameObject victoryPanel;
    public GameObject defeatPanel1P;
    public GameObject defeatPanel;
    public Text causeOfDeath;
    public GameObject pausePanel1P;
    public GameObject pausePanel;

    public AudioSource backgroundMusic;

    public Animator transitionPanel;

    public float returnDelay = 1f;
    public float deathAnimDuration = 1f;

    public bool singleplayer = true;

    bool won = false;
    bool lost = false;

    Player1P p1p;

    void Start()
    {
        singleplayer = (FindObjectOfType<NetworkManager>() == null);
        Time.timeScale = 1f;
        p1p = FindObjectOfType<Player1P>();
    }

    public void Victory()
    {
        if (!lost)
        {
            //Enabling a victory panel
            if(singleplayer)
                victoryPanel1P.SetActive(true);
            else
                victoryPanel.SetActive(true);
            //Turning off the gameplay music
            backgroundMusic.volume = 0f;
            p1p.canMove = false;
            p1p.GetComponent<Animator>().enabled = false;
            //We need to destroy the pause menu panel so the player can't pause once the game is technically over
            Destroy(pausePanel);
            Destroy(pausePanel1P);
            won = true;
            p1p.hasWonTheGame = true;
            Time.timeScale = 0f;
        }
    }

    public void Defeat(int cause)
    {
        if (!won)
        {   
            //Enabling a defeat panel
            Invoke("DefeatPanel", deathAnimDuration);
            if(singleplayer)
                defeatPanel1P.SetActive(true);
            else
                defeatPanel.SetActive(true);
            backgroundMusic.volume = 0f;
            Destroy(pausePanel);
            Destroy(pausePanel1P);
            lost = true;
            p1p.canMove = false;
            p1p.hasWonTheGame = true;
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
        Time.timeScale = 0f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        transitionPanel.Play("PanelOutro");
        Invoke("FinallyReturnToMenu", returnDelay);
    }

    void FinallyReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
