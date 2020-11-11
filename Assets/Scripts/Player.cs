using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    CharacterController controller;

    public static float speed = 20.0f; //  made this public and static in case it needs to be used by other stuff in the future

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        // normalize direction vector so speed doesnt increase on diagonals
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized; 

        controller.SimpleMove(Player.speed * dir);
    }
}
