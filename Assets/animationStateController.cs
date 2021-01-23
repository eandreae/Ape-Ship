using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
       bool isWalking = animator.GetBool(isWalkingHash);
       bool forwardPressed = Input.GetKey("w");

       if (!isWalking && forwardPressed)
       {
       	animator.SetBool(isWalkingHash, true);
       }
        
       if(isWalking && !forwardPressed)
       {
       	animator.SetBool(isWalkingHash, false);
       }
    }
}