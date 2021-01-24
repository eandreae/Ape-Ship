using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
   // int isWalkingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
       bool isWalking = animator.GetBool("isWalking");
       bool forwardPressed = Input.GetKey("w");

       if (!isWalking && forwardPressed)
       {
       	animator.SetBool("isWalking", true);
       }
        
       if(isWalking && !forwardPressed)
       {
       	animator.SetBool("isWalking", false);
       }
    }
}