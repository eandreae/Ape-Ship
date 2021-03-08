using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingAnim : MonoBehaviour
{
    private Animator animator;
    //private int hit = 0;
    public Text stomColor;
    string currColor;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currColor = stomColor.text;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Hit", 0);
        if(stomColor.text != currColor)
        {
            //Monkey damages stomach
            if(stomColor.text != "green")
            {
                currColor = stomColor.text; //save damaged color
            } else
            {
                animator.SetInteger("Hit", 1);
                currColor = stomColor.text;
            }
        }
    }
}
