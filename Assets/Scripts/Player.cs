//Use instead of Player script right to fix bugs
//Referenced through Aaron Hibberd's video on player movement: https://www.youtube.com/watch?v=sXQI_0ILEW4

//Using BeepBoopIndie's video on collecting coins: https://www.youtube.com/watch?v=XnKKaL5iwDM
//Also used Unity's official tutorial on collecting objects: https://learn.unity.com/tutorial/collecting-scoring-and-building-the-game?projectId=5c51479fedbc2a001fd5bb9f#5c7f8529edbc2a002053b78a


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
	private CharacterController controller;
    public float moveSpeed = 14f;
    public Vector3 dir;

    float defaultSpeed;
    public float health;
    public float oxygen;
    //public Text health_text;
    //public Text oxygen_text;
    public Text oxygen_color;
    public Text oxygen2_color;
    public bool invulnerable;
    private GameObject holdItem;
    public bool holding;
    private float invulnTime = 2;
    
    private Animator anim;
    public Animator oxygenCue;
    public Animator damageCue;
    // Moved camera functionality to PlayerCamera.cs
    // public Camera camera;

    public GameManager gm;
    public NetworkManager nm;

    public Slider healthBar;
    public Slider oxygenBar;

    //public AudioSource alarmSFX;
    private AudioSource walkingSFX;
    public AudioClip[] walkingSamples;
    
    public Collider gorillaCollider;
    public GameObject wpArrow;
    public GameObject CameraObj;

    public List<Transform> visibleTargets = new List<Transform>();
    private Transform highlightTarget;
    PlayerFOV targetsList;

    [HideInInspector]
    public bool canMove;

    [HideInInspector]
    public bool hasWonTheGame;

    Escape canTeleport;
    GameObject escapeObj;

    // Start is called before the first frame update
    void Start()
    {
       
        defaultSpeed = moveSpeed;
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        health = 3;
        oxygen = 60;
        invulnerable = false;
        holding = false;
        
        gm = FindObjectOfType<GameManager>();
        nm = FindObjectOfType<NetworkManager>();

        walkingSFX = this.GetComponent<AudioSource>();
        InvokeRepeating("PlayWalkingNoise", 0, 0.4f);

        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        
        oxygenBar = GameObject.Find("OxygenBar").GetComponent<Slider>();
        oxygen_color = GameObject.Find("OxygenColor").GetComponent<Text>();
        oxygen2_color = GameObject.Find("Oxygen2Color").GetComponent<Text>();

        oxygenCue = GameObject.Find("Oxygen Cue").GetComponent<Animator>();
        damageCue = GameObject.Find("Damage Cue").GetComponent<Animator>();

        escapeObj = GameObject.Find("Escape2");
        canTeleport = escapeObj.GetComponent<Escape>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canTeleport.teleport == true)
        {
            this.anim.Play("Idle");
            canMove = false;
        }

        // creating normalizing direction so that movement isnt faster on diagonals
        if (canMove && isLocalPlayer)
        {
            //Get list of targets from FieldOfView list
            targetsList = GetComponent<PlayerFOV>();
            //transfer each target into local list
            visibleTargets.Clear();
            foreach (Transform t in targetsList.visibleTargets)
            {
                visibleTargets.Add(t);
            }

            if (visibleTargets.Count == 0 || this.holding) {
                if (highlightTarget) {
                    highlightTarget.gameObject.GetComponent<ItemScript>().highlightOff();
                }
                highlightTarget = null;
            }

            if (Input.GetKeyDown("space") && !this.holdItem && visibleTargets.Count != 0)
            {
                this.holdItem = visibleTargets[0].gameObject;

                // Sets player to the pick-up item's parent so the item will move around with the player.            
                //other.gameObject.transform.parent = this.transform;
                visibleTargets[0].gameObject.GetComponent<ItemScript>().playerRoot = this.transform;

                // mark the coin (or whatever object) as picked up 
                visibleTargets[0].gameObject.GetComponent<ItemScript>().pickedUp = true;
                visibleTargets[0].gameObject.GetComponent<ItemScript>().thrown = false;

                // disable collision with held item
                Physics.IgnoreCollision(this.GetComponent<Collider>(), visibleTargets[0].gameObject.GetComponent<MeshCollider>(), true);

                this.wpArrow.SetActive(true);
                //other.gameObject.GetComponent<CoinScript>().pickedUp = true;
                StartCoroutine("PickUpCD");
            }
            this.dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

            if (dir.sqrMagnitude > 0)
            {
                //Debug.Log(this.holdItem);
                // if Player is holding an item, then use the hold animation. 
                if (this.holdItem)
                {
                    this.anim.Play("Hold");
                }
                else
                {
                    this.anim.Play("Walk"); // play walking animation when moving
                    this.holding = false; // if no holdItem, then holding must be false
                }
                this.transform.LookAt(transform.position + dir); // look in direction that player is walking
                controller.SimpleMove(this.moveSpeed * dir);
                //StartCoroutine("PlayWalkingNoise");
                // Moved camera functionality to PlayerCamera.cs
                // camera.transform.position = new Vector3(this.transform.position.x, 21.5f, this.transform.position.z - 10);
            }
            else
            {
                if (this.holdItem)
                {
                    this.anim.Play("Hold-Idle");
                }
                else
                {
                    this.anim.Play("Idle"); // if not moving, play idle anim
                    this.holding = false; // if no holdItem, then holding must be false
                }
            }

            if (!this.holding && visibleTargets.Count != 0 && visibleTargets[0] != highlightTarget) {
                if (highlightTarget) {
                    highlightTarget.gameObject.GetComponent<ItemScript>().highlightOff();
                }
                highlightTarget = visibleTargets[0];
                Debug.Log("target switched: "+highlightTarget.gameObject.name);

                highlightTarget.gameObject.GetComponent<ItemScript>().highlightOn();
            }
        }
        
        
        // Check if both oxygens are red.
        if ( oxygen_color.text == "red" && oxygen2_color.text == "red"){
            if ( oxygen > 0 ){ oxygen -= Time.deltaTime; }
            //If you update oxygen with a 0, the animation will play, otherwise it wont
            updateOxygen(0);
        // Check if one oxygen is red
        } else if (oxygen_color.text == "red" || oxygen2_color.text == "red")
        {
            if (oxygen > 0) { oxygen -= Time.deltaTime * 0.5f; }
            //If you update oxygen with a 0, the animation will play, otherwise it wont
            updateOxygen(0);
        }
        else {
            if ( oxygen < 90 ) {
                oxygen += Time.deltaTime * 2;
                updateOxygen(1);
            }
        }

    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "ThrownObject")
        {
            Debug.Log("Hit by object");
            health = health - 1 ;
            StartCoroutine("updateHealth");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        var pushPower = 10.0f;
        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
    
    public IEnumerator updateHealth() {
        damageCue.SetTrigger("DamageTrigger");
        healthBar.value = health;
        if ( health == 0 )
        { 
            Debug.Log("You Died!");
            //health_text.text = ""; 
            moveSpeed = 0f;
            gm.Defeat(2);
        }
        else
        {
            //playerHurtSFX.Play();
        }
        yield return new WaitForSeconds(0.2f); // get knocked by gorilla, then ignore collisions
        Physics.IgnoreCollision(gorillaCollider, GetComponent<Collider>(), true);
    }

    public void updateOxygen(int posOrNeg) {
        if (posOrNeg == 0){
            oxygenCue.SetTrigger("OxygenTrigger");
        }
        oxygenBar.value = Mathf.Floor(oxygen);
        //alarmSFX.Play();
        if ( Mathf.Floor(oxygen) == 0 ) 
        { 
            Debug.Log("You Died!");
            //oxygen_text.text = ""; 
            moveSpeed = 0f;
            gm.Defeat(1);
        }

    }

    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    IEnumerator PickUpCD(){
        yield return new WaitForSeconds(0.01f); // wait a brief moment before allowing dropping so code doesn't bug out
        this.holding = !this.holding;
    }

    void PlayWalkingNoise(){
        //Debug.Log(this.dir);
        if(this.dir.sqrMagnitude > 0 && health > 0 && !hasWonTheGame){
            AudioClip temp = this.walkingSamples[Random.Range(0, 3)];
            //Debug.Log(temp);
            this.walkingSFX.PlayOneShot(temp, Random.Range(0.01f, 0.05f));
        }
    }
}
