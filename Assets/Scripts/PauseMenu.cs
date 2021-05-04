using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool gameIsPaused = false;

    public GameObject pauseMenuPanel;
    public GameObject settingsPanel;

    //public Animator outroAnim;

    public AudioSource buttonPress;

    public AudioSource backgroundMusic;

    //public Minimap mm;

    public Player player;

    public Animator transitionPanel;

    void Start()
    {
        //mm = FindObjectOfType<Minimap>();
        //player = FindObjectOfType<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        buttonPress.Play();
        backgroundMusic.pitch = 0.75f;
        pauseMenuPanel.SetActive(true);
        gameIsPaused = true;
        //mm.canActivateMinimap = false;
        player.moveSpeed = 0f;
        //REMOVE THIS WHEN THE GAME BECOMES MULTIPLAYER
        Time.timeScale = 0f;
    }


    public void Resume()
    {
        buttonPress.Play();
        backgroundMusic.pitch = 1f;
        pauseMenuPanel.SetActive(false);
        gameIsPaused = false;
        //mm.canActivateMinimap = true;
        player.moveSpeed = 14f;
        //REMOVE THIS WHEN THE GAME BECOMES MULTIPLAYER
        Time.timeScale = 1f;
    }

    public void OpenSettings()
    {
        buttonPress.Play();
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void GoBackToMenu()
    {
        //REMOVE THIS WHEN THE GAME BECOMES MULTIPLAYER
        Time.timeScale = 1f;
        backgroundMusic.pitch = 1f;
        buttonPress.Play();
        pauseMenuPanel.SetActive(false);
        transitionPanel.Play("PanelOutro");
        Invoke("finallyGoBackToMenu", 1f);
    }

    void finallyGoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        //REMOVE THIS WHEN THE GAME BECOMES MULTIPLAYER
        Time.timeScale = 1f;
        backgroundMusic.pitch = 1f;
        buttonPress.Play();
        pauseMenuPanel.SetActive(false);
        transitionPanel.Play("PanelOutro");
        Invoke("finallyQuit", 1f);
    }

    void finallyQuit()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }
}
