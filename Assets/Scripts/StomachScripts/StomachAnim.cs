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
            animator.speed = 0.5f;
            animator.Play("Stomach|Hunger");
        }
        else if (stomColor.text == "red")
        {
            animator.speed = 1.0f;
            animator.Play("Stomach|Hunger");
        }
        else if (stomColor.text == "green")
        {
            animator.Play("Idle");
            animator.SetInteger("Color", 0);
        }
    }
}
