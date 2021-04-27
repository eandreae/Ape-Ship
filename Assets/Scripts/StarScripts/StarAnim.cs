using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarAnim : MonoBehaviour
{
    private Animator animator;
    public Text starColor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starColor.text == "yellow")
        {
            animator.SetInteger("Color", 1);

        }
        else if (starColor.text == "red")
        {
            animator.SetInteger("Color", 2);
        }
        else if (starColor.text == "green")
        {
            animator.SetInteger("Color", 0);
        }

    }
}
