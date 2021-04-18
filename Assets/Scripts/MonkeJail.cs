using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeJail : MonoBehaviour
{
    bool canPush = true;

    public float jailDuration;
    public float jailCooldown;

    public Animator[] jailBars;
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player" && canPush) || (other.gameObject.tag == "Gorilla" && other.GetComponent<GorillaMovement>().charging))// player/charging gorilla can push the button
        {
            CloseJail();
        }
    }

    void CloseJail()
    {
        foreach(Animator jailBar in jailBars)
        {
            jailBar.Play("CloseBar");
        }
        //The jail is now closed until a certain duration ends, and we can't close it again until the cooldown is over
        canPush = false;
        Invoke("OpenJail", jailDuration);
    }

    void OpenJail()
    {
        foreach (Animator jailBar in jailBars)
        {
            jailBar.Play("OpenBar");
        }
        Invoke("CanCloseJail", jailCooldown);
    }

    void CanCloseJail()
    {
        canPush = true;
    }
    

}
