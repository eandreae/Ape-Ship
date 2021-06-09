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
    [SyncVar] public float moveSpeed = 14f;
    public Vector3 dir;

    float defaultSpeed;
    public float health = 3;
    public float oxygen = 60;
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

        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        
        oxygenBar = GameObject.Find("OxygenBar").GetComponent<Slider>();
        oxygen_color = GameObject.Find("OxygenColor").GetComponent<Text>();
        oxygen2_color = GameObject.Find("Oxygen2Color").GetComponent<Text>();

        oxygenCue = GameObject.Find("Oxygen Cue").GetComponent<Animator>();
        damageCue = GameObject.Find("Damage Cue").GetComponent<Animator>();

        escapeObj = GameObject.Find("Escape2");
        canTeleport = escapeObj.GetComponent<Escape>();

        gm.alivePlayers.Add(this);

        if(isLocalPlayer) {
            InvokeRepeating("PlayWalkingNoise", 0, 0.4f); // only play footsteps for localplayer
            gm.localp = this;
            FindObjectOfType<PlayerCamera>().target = this.gameObject;
        }
        else {
            Object.Destroy(this.wpArrow); // get rid of waypoints for non-local players
        }
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
        if (isLocalPlayer)
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

            if (!this.holding && visibleTargets.Count != 0 && visibleTargets[0] != highlightTarget) {
                if (highlightTarget) {
                    highlightTarget.gameObject.GetComponent<ItemScript>().highlightOff();
                }
                highlightTarget = visibleTargets[0];
                Debug.Log("target switched: "+highlightTarget.gameObject.name);

                highlightTarget.gameObject.GetComponent<ItemScript>().highlightOn();
            }

            if (Input.GetKeyDown("space") && !this.holdItem && visibleTargets.Count != 0)
            {
                PickUp(visibleTargets[0].gameObject);
            }

            if (canMove)
            {
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
                else if (dir.sqrMagnitude == 0)
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
            }
            
            // Check if the player is invulnerable
            if (invulnerable)
            {
                if (invulnTime > 0)
                {
                    invulnTime -= Time.deltaTime;
                }
                else
                {
                    invulnerable = false;
                    Physics.IgnoreCollision(gorillaCollider, GetComponent<Collider>(), false);
                    invulnTime = 2;
                }
            }

            // code to drop items
            if (this.holding)
            {   // if player is holding an item and presses space bar
                if (Input.GetKeyDown("space")){
                    Drop();
                }
                // code to throw items
                else if (Input.GetKeyDown(KeyCode.LeftShift))
                { // holding item + press left shift
                    Throw();
                }
            }
            //if player isn't holding an item, reset to default speed
            if (!this.holding)
            {
                ChangeSpeed(defaultSpeed);
            }
        }
        // else { // handling death for non-local players
        //     if (this.health == 0){
        //         handleDeath(-1);
        //     }   
        // }

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

    void PickUp(GameObject item) {
        if(isServer){
            RpcPickUp(item);
        }
        else{
            CmdPickUp(item);
        }
    }

    [ClientRpc]
    void RpcPickUp(GameObject item) {
        this.holdItem = item;
        // Sets player to the pick-up item's parent so the item will move around with the player.            
        //other.gameObject.transform.parent = this.transform;
        item.GetComponent<ItemScript>().playerRoot = this.transform;

        // mark the coin (or whatever object) as picked up 
        item.GetComponent<ItemScript>().pickedUp = true;
        item.GetComponent<ItemScript>().thrown = false;

        // disable collision with held item
        Physics.IgnoreCollision(this.GetComponent<Collider>(), item.GetComponent<MeshCollider>(), true);

        if(isLocalPlayer)
            this.wpArrow.SetActive(true);
        //other.gameObject.GetComponent<CoinScript>().pickedUp = true;
        StartCoroutine("PickUpCD");
    }
    [Command]
    void CmdPickUp(GameObject item){
        RpcPickUp(item);
    }

    void Drop() {
        if(isServer){
            RpcThrow();
        }
        else{
            CmdThrow();
        }
    }
    
    [ClientRpc]
    void RpcDrop() {
        // Debug.Log("drop");
        // un-parent the player from the item
        //this.holdItem.transform.parent = null;
        // un-mark the coin as picked up.
        this.holdItem.GetComponent<ItemScript>().pickedUp = false;
        this.holdItem.GetComponent<ItemScript>().active = true; // set the item to active after being dropped
        
        // reenable collision
        Physics.IgnoreCollision(this.GetComponent<Collider>(), holdItem.gameObject.GetComponent<MeshCollider>(), false);
        //this.holdItem.GetComponent<CoinScript>().pickedUp = false;
        // get rid of hold item
        this.holdItem = null;

        if(isLocalPlayer)
            this.wpArrow.SetActive(false);

        StartCoroutine("PickUpCD");
    }
    [Command]
    void CmdDrop(){
        RpcDrop();
    }

    void Throw() {
        if(isServer){
            RpcThrow();
        }
        else{
            CmdThrow();
        }
    }

    [ClientRpc]
    void RpcThrow() {
        //this.holdItem.transform.parent = null; // unparent player from item
        this.holdItem.GetComponent<ItemScript>().pickedUp = false;
        this.holdItem.GetComponent<ItemScript>().active = true; // set the item to active after being dropped
        if(this.gameObject.name != "BatteryWithAnimations")
        {
            this.holdItem.GetComponent<Rigidbody>().velocity = (this.transform.forward * 20f + this.dir * 20f); // add velocity to thrown object <-- DOES NOT TAKE MASS INTO ACCOUNT
                                                                                                                //this.holdItem.GetComponent<Rigidbody>().AddForce(this.transform.forward * 20f - this.dir * 2, ForceMode.Impulse); // add force to thrown object <-- TAKES MASS INTO ACCOUNT
                                                                                                                //Debug.Log("throw");
            this.holdItem.GetComponent<ItemScript>().thrown = true;
            this.holdItem.GetComponent<Rigidbody>().isKinematic = false; // set object to non-kinematic so it can be thrown

        }
        Physics.IgnoreCollision(this.GetComponent<Collider>(), holdItem.gameObject.GetComponent<MeshCollider>(), false);
        // get rid of hold item
        this.holdItem = null;

        if(isLocalPlayer)
            this.wpArrow.SetActive(false);
        StartCoroutine("PickUpCD");
    }
    [Command]
    void CmdThrow(){
        RpcThrow();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "ThrownObject")
        {
            Debug.Log("Hit by object");
            StartCoroutine("updateHealth", true);
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
    
    public IEnumerator updateHealth(bool damage) { // taking damage from gorilla
        TakeDamage(damage);
        // else
        // {
        //     //playerHurtSFX.Play();
        // }
        yield return new WaitForSeconds(0.2f); // get knocked by gorilla, then ignore collisions
        Physics.IgnoreCollision(gorillaCollider, GetComponent<Collider>(), true);
    }

    void TakeDamage(bool damage){
        
        if(isLocalPlayer && damage)
            damageCue.SetTrigger("DamageTrigger"); // set damage trigger

        if(isServer){
            RpcTakeDamage(damage);
        }
        else {
            RpcTakeDamage(damage);
        }

    }

    [ClientRpc]
    void RpcTakeDamage(bool damage){
        if(damage)
            this.health--;

        healthBar.value = health;
        
        if ( health == 0 )
        { 
            handleDeath(2); // dying from updatehealth can only be from gorilla
        }
    }

    [Command]
    void CmdTakeDamage(bool damage){
        RpcTakeDamage(damage);
    }

    public void updateOxygen(int posOrNeg) {
        if(isServer){
            RpcUpdateOxygen(posOrNeg);
        } else {
            CmdUpdateOxygen(posOrNeg);
        }
        
        if ( Mathf.Floor(oxygen) == 0 ) 
        { 
            Debug.Log("You Died!");
            moveSpeed = 0f;
            handleDeath(1);
        }
    }

    [ClientRpc]
    void RpcUpdateOxygen(int posOrNeg){
        if (posOrNeg == 0){
            oxygenCue.SetTrigger("OxygenTrigger");
        }
        oxygenBar.value = Mathf.Floor(oxygen);
    }

    [Command]
    void CmdUpdateOxygen(int pn){
        RpcUpdateOxygen(pn);
    }

    public void handleDeath(int cause){
        this.GetComponent<Animator>().Play("PlayerDeath");
        Debug.Log("You Died!");
        //health_text.text = "";
        moveSpeed = 0f;

        // set player to untagged and remove player script from dead player
        this.gameObject.tag = "Untagged";
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        
        if(isLocalPlayer)
            gm.Defeat(cause);
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
