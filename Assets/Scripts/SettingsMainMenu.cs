using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMainMenu : MonoBehaviour
{
    public AudioSource buttonPress;

    public GameObject mainMenuPanel;
    public void ReturnToMenu()
    {
        buttonPress.Play();
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
