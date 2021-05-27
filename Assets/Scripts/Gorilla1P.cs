//referenced using Single Sapling Games' video on "Stop Enemy When close to Player - FPS Game in Unity - Part 57" : https://www.youtube.com/watch?v=26Oavz7WTC0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class Gorilla1P : MonoBehaviour
{
	float stoppingDistance = 15f;
    public float _SPEED = 6f;
    public float _ANGULAR_SPEED = 120f;
    public float _ACCELERATION = 8f;

	NavMeshAgent agent;

	GameObject target; // make sure gorilla has the same target on all clients/servers

    GameObject node1;
    GameObject node2;
    GameObject node3;
    GameObject node4;
    GameObject node5;
    GameObject node6;

    List<GameObject> nodes; 
    GameObject targetNode;
    GameObject playerObj;

    private bool canCharge = true;
    private bool canPickup = true;
    public bool charging = false;
    private bool playerLock = false;
    public bool stunned = false;
    private float playerDist = 0f;
    FieldOfView targetsList;
    FieldOfView objectsList;
    public List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> visibleObjects = new List<Transform>();
    public bool holdingObject = false;
    public GameObject objectHeld;

    public float chargeCooldown = 4f;


    // Start is called before the first frame update
    private void Start()
    {
        if (GameObject.FindObjectOfType<NetworkManager>()){
            Object.Destroy(this.gameObject);
        }

        agent = GetComponent<NavMeshAgent>();
        nodes = new List<GameObject>();
        node1 = GameObject.FindGameObjectWithTag("Stomach");
        nodes.Add(node1);
        node2 = GameObject.FindGameObjectWithTag("ElecControl");
        nodes.Add(node2);
        node3 = GameObject.FindGameObjectWithTag("Nav");
        nodes.Add(node3);
        node4 = GameObject.FindGameObjectWithTag("Reactor");
        nodes.Add(node4);
        node5 = GameObject.FindGameObjectWithTag("O2");
        nodes.Add(node5);
        node6 = GameObject.FindGameObjectWithTag("O2_2");
        nodes.Add(node6);

        // foreach (GameObject g in nodes)
        //     Debug.Log(g);
        //agent.speed = _SPEED;
        //agent.acceleration = _ACCELERATION;
        //agent.angularSpeed = _ANGULAR_SPEED;
        
        int targetnum = Random.Range(0, nodes.Count-1);
        targetNode = nodes[targetnum];
        target = targetNode;
        Debug.Log("Gorilla moving to: " + target);
    }


    // Update is called once per frame
    private void Update()    
    {
        if(target == null)
            FindNewTarget();
        //Get list of targets from FieldOfView list
        targetsList = GetComponent<FieldOfView>();
        //transfer each target into local list
        visibleTargets.Clear();
        foreach (Transform t in targetsList.visibleTargets)
        {
            visibleTargets.Add(t);
        }

        //Get list of targets from FieldOfView list
        objectsList = GetComponent<FieldOfView>();
        //transfer each target into local list
        visibleObjects.Clear();
        foreach (Transform t in objectsList.visibleObjects)
        {
            visibleObjects.Add(t);
        }

        /* ------------------ Temp Disabled -----------------------------
        // if gorilla is not following player, check for the player distance
        if (visibleObjects.Count != 0 && !holdingObject)
        {
            Debug.Log("Gorilla has found object");
            target = visibleObjects[0].gameObject;
            stoppingDistance = 0;
        }
        else         -------------------------------------------------------------------------------
        */
        if (visibleTargets.Count != 0)
        {
            //Debug.Log("Gorilla has locked on Player");
            playerLock = true;
            stoppingDistance = 0; // make stopping distance 0 if tracking the player
            target = visibleTargets[0].gameObject;

        }



        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist < stoppingDistance)
        {
        	FindNewTarget();
        }
        //if the gorilla lost sight of the player
        else if (playerLock == true && visibleTargets.Count == 0)
        {
            playerLock = false;
            stoppingDistance = 15f;
            FindNewTarget();
        }
        else
        {
        	GoToTarget();
        }
    }

    private void FindNewTarget()
    {
        agent.isStopped = true;
        int targetnum = Random.Range(0, nodes.Count - 1);
        targetNode = nodes[targetnum];
        target = targetNode;
        Debug.Log("Gorilla moving to: " + target);
    }

    private void GoToTarget()
    {
        // If gorilla is CHASING PLAYER, HAS CHARGE CD, and is CLOSE TO PLAYER, it will charge
        if (target.tag == "Player" && holdingObject)
        {
            StartCoroutine("ThrowObject");
        }
         else if (target.tag == "Player" && canCharge && playerLock && !this.stunned && !holdingObject){
            //Debug.Log("Gorilla is charging");
            StartCoroutine("ChargeAttack");
            canCharge = false;
        }
        else if (!charging){
       	    agent.isStopped = false;
       	    agent.SetDestination(target.transform.position);
        }
    }

    private void StopEnemy() // code executed when gorilla reaches its target.
    {
      	agent.isStopped = true;
        //get new target
        //Start();
        if(target == targetNode){ // search for a new target node if gorilla reaches a target node
            int targetnum = Random.Range(0, nodes.Count-1);
            targetNode = nodes[targetnum];
            target = targetNode;
        }
    }

    

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "PlayerMod") { // use player model, not player trigger
            if (!this.stunned) {
                StartCoroutine("AttackPlayer",  other.transform.parent.GetComponent<Player1P>());
            }
        } 
        else if (other.tag != "Player" && other.gameObject == target && !holdingObject)
        {
            PickUpObject(other);
        } 
        else if (other.tag == "Pick Up" && !stunned) {
            if (other.gameObject.GetComponent<ItemScript>().type == "Banana" && other.gameObject.GetComponent<ItemScript>().active){
                this.stunned = true;
                StartCoroutine("SelfStun");
            }
            else if (other.gameObject.GetComponent<ItemScript>().type == "Nuke" && other.gameObject.GetComponent<ItemScript>().active){
                this.stunned = true;
                StartCoroutine("KnockBack", other.transform.position);
            }
        }
    }
    
    private void PickUpObject(Collider other)
    {
        other.transform.parent = this.transform;
        target.GetComponent<Rigidbody>().isKinematic = true;
        holdingObject = true;
        canPickup = false;
        objectHeld = target;
        FindNewTarget();
    }
    // this coroutine manages the Gorilla ChargeAttack.
    // It ends if the gorilla stops chasing the player.
    // If the gorilla can charge, it will wait for 1.5s, and charge for 1.5s, then self-stun for .5s.
    // charge cooldown is 5 seconds.
    IEnumerator ChargeAttack(){
        if (canCharge){ // some condition here to initiate the charge attack (maybe also consider player distance?)
            charging = true;

            StopGorilla();
            this.transform.LookAt(target.transform.position);
            this.GetComponent<Rigidbody>().ResetInertiaTensor(); // reset inertia before charging

            yield return new WaitForSeconds(0.75f); // time in seconds to wait
            
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 700f , ForceMode.Impulse);

            yield return new WaitForSeconds(1f); // charge for 1 second
            
            this.GetComponent<Rigidbody>().velocity = Vector3.zero; // remove forces on gorilla so he stops
            this.GetComponent<Rigidbody>().isKinematic = true;
            charging = false;
            //agent.speed = 0; // stops
            //agent.isStopped = true;

            yield return new WaitForSeconds(0.5f); // gorilla self-stun after it charges

            StartGorilla();
        
            yield return new WaitForSeconds(chargeCooldown); // charge cooldown
            canCharge = true;
        }
    }

    IEnumerator ThrowObject()
    {
        if (holdingObject)
        {
            agent.speed = 0;
            gameObject.transform.LookAt(playerObj.transform.position);
            yield return new WaitForSeconds(0.5f); // wait for 1 second

            objectHeld.GetComponent<Rigidbody>().isKinematic = false;
            objectHeld.tag = "ThrownObject";
            objectHeld.GetComponent<Rigidbody>().AddForce(this.transform.forward * 3f, ForceMode.Impulse);

            yield return new WaitForSeconds(1f); // wait for 1 second

            holdingObject = false;
            StartCoroutine("ThrowWait");
            StartCoroutine("DestroyThrown", objectHeld);
            StartGorilla();
        }
    }

    IEnumerator ThrowWait()
    {
        yield return new WaitForSeconds(5f); // wait for 5 seconds
        canPickup = true;
    }

    IEnumerator DestroyThrown(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }

    // This coroutine handles part of the Gorilla/Player collision interaction.
    // If the Gorilla hits the player, he should wait for a little bit before moving again. 
    IEnumerator AttackPlayer(Player1P player){
        Debug.Log("ATTACK PLAYER");
        this.stunned = true;
        this.charging = false;  // in case gorilla was charging
        this.canCharge = false;
        
        StopCoroutine("ChargeAttack");  // stop charging
        StopGorilla();
        
        this.GetComponent<Rigidbody>().velocity = Vector3.zero; // remove forces on gorilla so he stops
        this.GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("Hit the Gorilla!");
            // Subtract one from the health of the Player.
        if(!player.invulnerable){
            player.health--;
            // Make the player temporarily invulnerable
            player.invulnerable = true;
            player.gorillaCollider = this.GetComponent<Collider>();
            // Update the health of the player.
            player.StartCoroutine("updateHealth");
        }
        
        //animator.play("attack-anim"); // play the gorilla attack animation (?)
        yield return new WaitForSeconds(0.75f); // wait
        
        StartGorilla();
        this.stunned = false;

        if(!this.canCharge){    // if gorilla was in the middle of his charge
            yield return new WaitForSeconds(4f); // reset charge cd
            this.canCharge = true;
        }
    }
    
    IEnumerator SelfStun(){ // from banana
        //Debug.Log("GORILLA STUNNED");

        this.stunned = true;
        this.charging = false;  // in case gorilla was charging
        this.canCharge = false;
        
        StopCoroutine("ChargeAttack");  // stop charging
        StopCoroutine("AttackPlayer");  // stop attackplayer coroutine in case of overlap
        StopCoroutine("ThrowObject");
        StopGorilla();        
        
        this.GetComponent<Rigidbody>().velocity = Vector3.zero; // remove forces on gorilla so he stops
        this.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(2f); // banana stuns gorilla for 2 seconds
        
        StartGorilla();
        this.stunned = false;
        
        if(!this.canCharge){    // if gorilla was in the middle of his charge
            yield return new WaitForSeconds(4f); // reset charge cd
            this.canCharge = true;
        }
    }

    IEnumerator KnockBack(Vector3 impactPos){ // from nuke soda; impact position is the soda position
        Debug.Log("GORILLA KNOCKED BACK");

        StopCoroutine("ChargeAttack");  // stop charging
        StopCoroutine("AttackPlayer");  // stop attackplayer coroutine in case of overlap
        StopCoroutine("SelfStun");
        
        StopGorilla();
        
        this.charging = false;  // in case gorilla was charging
        this.canCharge = false; 
        //this.stunned = true;

        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero; // remove forces on gorilla so he stops

        Vector3 pushDir = this.transform.position - impactPos; // find vector to push gorilla away
        this.GetComponent<Rigidbody>().AddForce(pushDir * 50, ForceMode.Impulse); // add a small push to the rigidbody
        
        yield return new WaitForSeconds(1.5f); // nuke explodes, stunning gorilla for 1.5 second
        this.GetComponent<Rigidbody>().velocity = Vector3.zero; // remove forces on gorilla so he stops
        this.GetComponent<Rigidbody>().isKinematic = true;
        
        StartGorilla();
        this.stunned = false;
        
        if(!this.canCharge){    // if gorilla was in the middle of his charge
            yield return new WaitForSeconds(4f); // reset charge cd
            this.canCharge = true;
        }
    }


    private void StopGorilla() {
        agent.isStopped = true;
        agent.speed = 0;
        agent.acceleration = 100;
        agent.angularSpeed = 15; // decrease the angular speed so it doesn't turn as much
    }

    private void StartGorilla(){
        agent.speed = _SPEED;
        agent.angularSpeed = _ANGULAR_SPEED;
        agent.acceleration = _ACCELERATION;
        agent.isStopped = false;
    }
}
