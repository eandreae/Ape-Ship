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

    public float returnDelay = 1f;

    public void Victory()
    {
        //Enabling a victory panel
        victoryPanel.SetActive(true);
        //Turning off the gameplay music
        backgroundMusic.volume = 0f;
        //We need to destroy the pause menu panel so the player can't pause once the game is technically over
        Destroy(pausePanel);
    }

    public void Defeat()
    {
        //Enabling a defeat panel
        defeatPanel.SetActive(true);
        backgroundMusic.volume = 0f;
        Destroy(pausePanel);
    }
    public void ReturnToMenu()
    {
        Invoke("FinallyReturnToMenu", returnDelay);
        //Gonna put another transition panel, hence the delay
    }

    void FinallyReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
