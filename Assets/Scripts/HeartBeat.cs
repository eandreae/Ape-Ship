using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartBeat : MonoBehaviour
{
    private Animator animator;
    private int color = 0;
    public Text heartColor;
    GameObject heartObj;

    void Start()
    {
       animator = GetComponent<Animator>();
       heartObj = GameObject.FindGameObjectWithTag("Reactor");
    }

    void Update()
    {
        //Debug.Log(injured);
        if(heartColor.text == "red" && color != 2)
        {
            animator.SetInteger("Color", 2);
            color = 2;
        }
        else if (heartColor.text == "green" && color != 0)
        {
            animator.SetInteger("Color", 0);
            color = 0;
        }
        else if (heartColor.text == "yellow" && color != 1)
        {
            animator.SetInteger("Color", 1);
            color = 1;
        }

    }
}
