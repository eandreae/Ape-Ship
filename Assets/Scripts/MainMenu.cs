using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float startDelay = 1f;

    public Animator startGameAnim;

    public AudioSource buttonPress;

    public GameObject mainMenuPanel;
    public GameObject playOptionsPanel;
    public GameObject hostOptionsPanel;
    public GameObject tutorialPanel;
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    public void StartGame()
    {
        Debug.Log("Start the game!");
        //Play an animation that leads into the game using a UI panel
        startGameAnim.Play("PanelOutro");
        //This method will actually transition the game into the next scene
        Invoke("finallyStart", startDelay);
        //Plays a button press sound effect
        buttonPress.Play();
    }

    public void HostGame() {
        // code to set up or join a lobby
        buttonPress.Play();
    }

    // Load the play options screen
    public void ShowPlayOptions() {
        playOptionsPanel.SetActive(true);
        buttonPress.Play();
        mainMenuPanel.SetActive(false);
    }

    // hide play options
    public void HidePlayOptions() {
        playOptionsPanel.SetActive(false);
        buttonPress.Play();
        mainMenuPanel.SetActive(true);
    }

    public void ShowMultiPlayOptions()
    {
        hostOptionsPanel.SetActive(true);
        buttonPress.Play();
        playOptionsPanel.SetActive(false);
    }

    public void HideMultiPlayOptions()
    {
        hostOptionsPanel.SetActive(false);
        buttonPress.Play();
        playOptionsPanel.SetActive(true);
    }

    void finallyStart()
    {
        SceneManager.LoadScene("game");
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
