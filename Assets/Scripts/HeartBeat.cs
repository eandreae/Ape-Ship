using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartBeat : MonoBehaviour
{
    private Animator animator;
    private bool injured = false;
    public Text heartColor;
    GameObject heartObj;

    void Start()
    {
       animator = GetComponent<Animator>();
       heartObj = GameObject.FindGameObjectWithTag("Reactor");
    }

    void Update()
    {
        Debug.Log(injured);
        if(heartColor.text == "red" && !injured)
        {
            animator.SetBool("Injured", true);
            injured = true;
        }
        else if (heartColor.text == "green" || heartColor.text == "yellow" && injured)
        {
            animator.SetBool("Injured", false);
            injured = false;
        }
    }
}
