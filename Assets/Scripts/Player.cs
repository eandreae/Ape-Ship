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
    public bool holding;
    private float invulnTime = 2;
    private CharacterController controller;
    private Animator anim;
    public Animator cues;
    // Moved camera functionality to PlayerCamera.cs
    // public Camera camera;
    private GameObject holdItem;

    public GameManager gm;

    public Slider healthBar;
    public Slider oxygenBar;

    public AudioSource alarmSFX;
    public Vector3 dir;
    public AudioSource walkingSFX;
    public AudioClip[] walkingSamples;

    // Start is called before the first frame update
    void Start()
    {
    	moveSpeed = 14f;
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        health = 3;
        oxygen = 60;
        invulnerable = false;
        holding = false;
        gm = FindObjectOfType<GameManager>();
        walkingSFX = this.GetComponent<AudioSource>();
        InvokeRepeating("PlayWalkingNoise", 0, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        //checks to see if pressing any arrow keys
        //if so will go horizontal if left or right
        //will go vertical if up or down
        //transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);
        
        // creating normalizing direction so that movement isnt faster on diagonals
        this.dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized; 
        if (dir.sqrMagnitude > 0){
            //Debug.Log(this.holdItem);
            // if Player is holding an item, then use the hold animation. 
            if(this.holdItem){
                this.anim.Play("Hold");
            }
            else{
                this.anim.Play("Walk"); // play walking animation when moving
                this.holding = false; // if no holdItem, then holding must be false
            }
            this.transform.LookAt(transform.position + dir); // look in direction that player is walking
            controller.SimpleMove(this.moveSpeed * dir);
            //StartCoroutine("PlayWalkingNoise");
            // Moved camera functionality to PlayerCamera.cs
            // camera.transform.position = new Vector3(this.transform.position.x, 21.5f, this.transform.position.z - 10);
        }
        else if (dir.sqrMagnitude == 0){
            if(this.holdItem){
                this.anim.Play("Hold-Idle");
            }
            else{
                this.anim.Play("Idle"); // if not moving, play idle anim
                this.holding = false; // if no holdItem, then holding must be false
            }
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

        // code to drop items
        if(this.holding && Input.GetKeyDown("space")){ // if player is holding an item and presses space bar
            // un-parent the player from the item
            this.holdItem.transform.parent = null;
            // un-mark the coin as picked up.
            this.holdItem.GetComponent<ItemScript>().pickedUp = false;
            this.holdItem.GetComponent<ItemScript>().active = true; // set the item to active after being dropped
            //this.holdItem.GetComponent<CoinScript>().pickedUp = false;
            // get rid of hold item
            this.holdItem = null;
            this.holding = false;
        }
        
        // Check if the oxygen color is red.
        if ( oxygen_color.text == "red" ){
            if ( oxygen > 0 ){ oxygen -= Time.deltaTime; }
            updateOxygen();
        }
        else {
            if ( oxygen < 60 ) {
                oxygen += Time.deltaTime * 2;
                updateOxygen();
            }
        }
    }

    //checks to see if picked up object, activated everytime touch a trigger collider
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Collided with something!");

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

    // by using OnTriggerStay, we can check for picking up as long as player is touching the item.
    void OnTriggerStay(Collider other){
        //test tag, if string is same as pick up...
    	if (!this.holdItem && other.gameObject.CompareTag("Pick Up") && Input.GetKeyDown("space"))
    	{
            this.holdItem = other.gameObject;
    		
            //deactivates game object
    		//other.gameObject.SetActive(false);

            // Sets player to the pick-up item's parent so the item will move around with the player.            
            other.gameObject.transform.parent = this.transform;

            // mark the coin (or whatever object) as picked up 
            other.gameObject.GetComponent<ItemScript>().pickedUp = true;
            //other.gameObject.GetComponent<CoinScript>().pickedUp = true;
            StartCoroutine("PickUpCD");
            //Debug.Log(this.holdItem);
    	}
    }
    
    //private void OnGUI(){
    	//GUI.Label(new Rect(10, 10, 100, 20), "Bananas : " + points);
    //}

    public void updateHealth() {
        cues.SetTrigger("DamageTrigger");
        healthBar.value = health;
        if ( health == 0 )
        { 
            Debug.Log("You Died!");
            health_text.text = ""; 
            moveSpeed = 0f;
            gm.Defeat();
        }
    }

    public void updateOxygen() {
        cues.SetTrigger("OxygenTrigger");
        oxygenBar.value = Mathf.Floor(oxygen);
        //alarmSFX.Play();
        if ( Mathf.Floor(oxygen) == 0 ) 
        { 
            Debug.Log("You Died!");
            oxygen_text.text = ""; 
            moveSpeed = 0f;
            gm.Defeat();
        }

    }

    IEnumerator PickUpCD(){
        yield return new WaitForSeconds(0.1f); // wait a brief moment before allowing dropping so code doesn't bug out
        this.holding = true;
    }

    void PlayWalkingNoise(){
        //Debug.Log(this.dir);
        if(this.dir.sqrMagnitude > 0){
            AudioClip temp = this.walkingSamples[Random.Range(0, 3)];
            //Debug.Log(temp);
            this.walkingSFX.PlayOneShot(temp, Random.Range(0.01f, 0.05f));
        }
    }
}
