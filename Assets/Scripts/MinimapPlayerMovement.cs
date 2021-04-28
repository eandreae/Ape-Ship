using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerMovement : MonoBehaviour
{
    //Uncomment this if the player's sprite is ever replaced
    //public SpriteRenderer playerSprite;

    public float moveSpeed = 5f;

    Rigidbody2D rb;

    Vector2 movement;

    public CharacterController pcc;

    public Vector3 velocityCheck = new Vector3(0.05f, 0.05f, 0.05f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = pcc.velocity.x;
        movement.y = pcc.velocity.z;
        Debug.Log(pcc.velocity);
    }

    void FixedUpdate()
    {
        if (pcc.velocity.sqrMagnitude > velocityCheck.sqrMagnitude)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
