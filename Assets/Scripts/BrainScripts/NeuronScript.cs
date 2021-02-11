using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NeuronScript : MonoBehaviour
{
    private Animator animator;
    private int color = 0;
    public Text brainColor;
    GameObject brainObj;

    void Start()
    {
        animator = GetComponent<Animator>();
        brainObj = GameObject.FindGameObjectWithTag("Nav");
    }

    void Update()
    {
        //Debug.Log(injured);
        if (brainColor.text == "red" && color != 2)
        {
            animator.SetInteger("Color", 2);
            color = 2;
        }
        else if (brainColor.text == "green" && color != 0)
        {
            animator.SetInteger("Color", 0);
            color = 0;
        }
        else if (brainColor.text == "yellow" && color != 1)
        {
            animator.SetInteger("Color", 1);
            color = 1;
        }

    }
}