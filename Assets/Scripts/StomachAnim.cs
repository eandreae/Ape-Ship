using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StomachAnim : MonoBehaviour
{
    private Animator animator;
    private int color = 0;
    public Text stomColor;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stomColor.text == "yellow")
        {
            animator.SetInteger("Color", 1);
            color = 1;
            animator.speed = 0.5f;
        }
        else if (stomColor.text == "red")
        {
            animator.SetInteger("Color", 2);
            color = 2;
            animator.speed = 1.0f;
        }
        else if (stomColor.text == "green")
        {
            animator.SetInteger("Color", 0);
            color = 0;
        }
    }
}
