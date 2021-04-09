using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LungAnim : MonoBehaviour
{
    private Animator animator;

    public Text lungColor;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lungColor.text == "yellow")
        {
            animator.speed = 1.5f;

        }
        else if (lungColor.text == "red")
        {
            animator.speed = 2.0f;

        }
        else if (lungColor.text == "green")
        {
            animator.speed = 1.0f;
        }

    }
}
