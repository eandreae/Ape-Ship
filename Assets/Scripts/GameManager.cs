using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject pausePanel;

    public AudioSource backgroundMusic;

    public Animator transitionPanel;

    public float returnDelay = 1f;

    bool won = false;
    bool lost = false;

    public void Victory()
    {
        if (!lost)
        {
            //Enabling a victory panel
            victoryPanel.SetActive(true);
            //Turning off the gameplay music
            backgroundMusic.volume = 0f;
            //We need to destroy the pause menu panel so the player can't pause once the game is technically over
            Destroy(pausePanel);
            won = true;
        }
    }

    public void Defeat()
    {
        if (!won)
        {
            //Enabling a defeat panel
            defeatPanel.SetActive(true);
            backgroundMusic.volume = 0f;
            Destroy(pausePanel);
            lost = true;
        }
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
