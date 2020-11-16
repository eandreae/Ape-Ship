//Use instead of Player script right to fix bugs
//Referenced through Aaron Hibberd's video on player movement: https://www.youtube.com/watch?v=sXQI_0ILEW4

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
    	moveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        //checks to see if pressing any arrow keys
        //if so will go horizontal if left or right
        //will go vertical if up or down
        	transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);
        
    }
}
