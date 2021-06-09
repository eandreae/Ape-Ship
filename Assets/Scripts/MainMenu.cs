using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;

public class MainMenu : MonoBehaviour
{
    public float startDelay = 1f;

    public Animator startGameAnim;
    public Animator whaleAnim;
    public Animator apesShipAnim;
    public Animator hostTextAnim;

    public AudioSource buttonPress;

    public GameObject mainMenuPanel;
    public GameObject mainMenuPanel2;

    public GameObject playOptionsPanel;
    public GameObject tutorialPanel;
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        Object.Destroy(GameObject.Find("NetworkManager"));
        Debug.Log("Start the game!");
        //Play an animation that leads into the game using a UI panel
        startGameAnim.Play("MenuPanelOutro");
        //This method will actually transition the game into the next scene
        Invoke("finallyStart", startDelay);
        //Plays a button press sound effect
        buttonPress.Play();
        whaleAnim.Play("WhaleChaseOne");
        apesShipAnim.Play("WhaleChaseTwo");
    }

    public void StartMultiplayer()
    {
        //Plays a button press sound effect
        buttonPress.Play();
        if (SteamAPI.IsSteamRunning())
        {
            whaleAnim.Play("WhaleChaseOne");
            apesShipAnim.Play("WhaleChaseTwo");
        }
        else
            hostTextAnim.Play("HostTextSlideIn");

    }

    public void HostAnimation()
    {
        
    }

    // Load the play options screen
    public void ShowPlayOptions() {
        playOptionsPanel.SetActive(true);
        buttonPress.Play();
        mainMenuPanel2.SetActive(false);
    }

    // hide play options
    public void HidePlayOptions() {
        playOptionsPanel.SetActive(false);
        buttonPress.Play();
        mainMenuPanel2.SetActive(true);
    }

    void finallyStart()
    {
        SceneManager.LoadScene("game");
    }

    public void changeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void Settings()
    {
        Debug.Log("Go to settings screen!");
        //Sets the settings panel to be visible
        settingsPanel.SetActive(true);
        buttonPress.Play();
        mainMenuPanel.SetActive(false);
    }

    public void Tutorial()
    {
        Debug.Log("Go to tutorial screen!");
        //Sets the tutorial panel to be visisble
        tutorialPanel.SetActive(true);
        buttonPress.Play();
        mainMenuPanel.SetActive(false);
    }

    public void Credits()
    {
        //Sets the credits panel to be visible
        creditsPanel.SetActive(true);
        buttonPress.Play();
        mainMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game!");
        startGameAnim.Play("PanelOutro");
        Invoke("finallyQuit", startDelay);
        buttonPress.Play();
    }

    void finallyQuit()
    {
        Application.Quit();
    }

    
}
