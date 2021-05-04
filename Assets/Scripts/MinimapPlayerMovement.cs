using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerMovement : MonoBehaviour
{
    //Uncomment this if the player's sprite is ever replaced
    //public SpriteRenderer playerSprite;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    public CharacterController pcc;

    // Update is called once per frame
    void Update()
    {
        if (pcc.velocity != Vector3.zero)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        //Uncomment this section of the player's sprite is ever replaced
        /*
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            playerSprite.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSprite.flipX = true;
        }*/

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
