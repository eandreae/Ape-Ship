using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeJail : MonoBehaviour
{
    bool canPush = true;

    public float jailDuration;
    public float jailCooldown;

    public Animator[] jailBars;
    public Animator interactAnim;

    void OnTriggerEnter(Collider other)
    {
        if (canPush)
        {
            interactAnim.Play("PickUpTextRaise");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (canPush)
        {
            interactAnim.Play("PickUpTextLower");
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && canPush)
        {
            CloseJail();
        }
    }

    void CloseJail()
    {
        interactAnim.Play("PickUpTextLower");
        foreach (Animator jailBar in jailBars)
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
