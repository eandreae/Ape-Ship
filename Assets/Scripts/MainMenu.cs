using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float startDelay = 1f;

    public Animator startGameAnim;

    public AudioSource buttonPress;

    public GameObject tutorialPanel;
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

    void finallyStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        Debug.Log("Go to settings screen!");
        //Sets the settings panel to be visible
        settingsPanel.SetActive(true);
        buttonPress.Play();
    }

    public void Tutorial()
    {
        Debug.Log("Go to tutorial screen!");
        //Sets the tutorial panel to be visisble
        tutorialPanel.SetActive(true);
        buttonPress.Play();
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
