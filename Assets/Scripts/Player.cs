//Use instead of Player script right to fix bugs
//Referenced through Aaron Hibberd's video on player movement: https://www.youtube.com/watch?v=sXQI_0ILEW4

//Using BeepBoopIndie's video on collecting coins: https://www.youtube.com/watch?v=XnKKaL5iwDM
//Also used Unity's official tutorial on collecting objects: https://learn.unity.com/tutorial/collecting-scoring-and-building-the-game?projectId=5c51479fedbc2a001fd5bb9f#5c7f8529edbc2a002053b78a


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float moveSpeed;
    public int points;
    public float health;
    public float oxygen;
    public Text health_text;
    public Text oxygen_text;
    public Text oxygen_color;
    private bool invulnerable;
    private float invulnTime = 2;
    private CharacterController controller;
    private Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
    	moveSpeed = 14f;
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        health = 3;
        oxygen = 60;
        invulnerable = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks to see if pressing any arrow keys
        //if so will go horizontal if left or right
        //will go vertical if up or down
        //transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);
        
        // creating normalizing direction so that movement isnt faster on diagonals
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized; 
        if (dir.sqrMagnitude > 0){
            this.anim.Play("Walk"); // play walking animation when moving
            //this.transform.LookAt(dir); // look in direction that play is walking
            controller.SimpleMove(this.moveSpeed * dir);
        }
        else if (dir.sqrMagnitude == 0){
            this.anim.Play("Idle"); // if not moving, play idle anim
        }
        // Check if the player is invulnerable
        if ( invulnerable ){
            if ( invulnTime > 0 ){
                invulnTime -= Time.deltaTime;
            }
            else {
                invulnerable = false;
                invulnTime = 2;
            }
        }
        
        // Check if the oxygen color is red.
        if ( oxygen_color.text == "red" ){
            Debug.Log("Oxygen is red!");
            if ( oxygen > 0 ){ oxygen -= Time.deltaTime; }
            updateOxygen();
        }
        else {
            if ( oxygen < 60 ) {
                oxygen += Time.deltaTime;
                updateOxygen();
            }
        }
    }

    //checks to see if picked up object, activated everytime touch a trigger collider
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Collided with something!");

    	//test tag, if string is same as pick up...
    	if (other.gameObject.CompareTag("Pick Up"))
    	{
    		//deactivates game object
    		other.gameObject.SetActive(false);
    	}

        if (other.gameObject.CompareTag("Gorilla") && !invulnerable )
        {
            Debug.Log("Hit the Gorilla!");
            // Subtract one from the health of the Player.
            health--;
            // Make the player temporarily invulnerable
            invulnerable = true;
            // Update the health of the player.
            updateHealth();
        }
    }
    
    private void OnGUI(){
    	GUI.Label(new Rect(10, 10, 100, 20), "Bananas : " + points);
    }

    public void updateHealth() {

        if ( health == 3 ){ health_text.text = "[] [] []"; }
        if ( health == 2 ){ health_text.text = "[] []   "; }
        if ( health == 1 ){ health_text.text = "[]      "; }
        if ( health == 0 )
        { 
            Debug.Log("You Died!");
            health_text.text = ""; 
            moveSpeed = 0f;
        }
    }

    public void updateOxygen() {

        if ( Mathf.Floor(oxygen) == 60 ) { oxygen_text.text = "[] [] [] [] []"; }
        if ( Mathf.Floor(oxygen) == 48 ) { oxygen_text.text = "[] [] [] []   "; }
        if ( Mathf.Floor(oxygen) == 36 ) { oxygen_text.text = "[] [] []      "; }
        if ( Mathf.Floor(oxygen) == 24 ) { oxygen_text.text = "[] []         "; }
        if ( Mathf.Floor(oxygen) == 12 ) { oxygen_text.text = "[]            "; }
        if ( Mathf.Floor(oxygen) == 0 ) 
        { 
            Debug.Log("You Died!");
            oxygen_text.text = ""; 
            moveSpeed = 0f;
        }

    }
}
