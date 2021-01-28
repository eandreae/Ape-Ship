using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    public AudioSource backgroundMusic;

    public float returnDelay = 1f;

    public void Victory()
    {
        //Enabling a victory panel
        victoryPanel.SetActive(true);
        //Turning off the gameplay music
        backgroundMusic.volume = 0f;
    }

    public void Defeat()
    {
        //Enabling a defeat panel
        defeatPanel.SetActive(true);
        backgroundMusic.volume = 0f;
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
