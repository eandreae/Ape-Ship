using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public AudioSource buttonPress;
    public void ReturnToMenu()
    {
        buttonPress.Play();
        gameObject.SetActive(false);
    }
}
