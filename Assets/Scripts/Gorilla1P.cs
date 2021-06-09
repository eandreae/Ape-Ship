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
    private GameObject fakeHolding;

    public float chargeCooldown = 4f;
    private bool startMoving = false;
    private AudioSource audioData;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    private bool carrying = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (GameObject.FindObjectOfType<NetworkManager>()){
            Object.Destroy(this.gameObject);
        }

        audioData = GetComponent<AudioSource>();

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

        StartCoroutine("BeginningWait");

    }


    // Update is called once per frame
    private void Update()    
    {
        if (startMoving)
        {
            if (target == null)
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

            /* ------------------ Temp Disabled -----------------------------*/
            // if gorilla is not following player, check for the player distance
            if (visibleObjects.Count != 0 && !holdingObject && canPickup)
            {
                Debug.Log("Gorilla has found object");
                target = visibleObjects[0].gameObject;
                stoppingDistance = 0;
            }
            else if (visibleTargets.Count != 0)
            {
                //Debug.Log("Gorilla has locked on Player");
                playerLock = true;
                stoppingDistance = 0; // make stopping distance 0 if tracking the player
                target = visibleTargets[0].gameObject;

            }

            if (this.holdingObject && !carrying)
            {
                this.GetComponent<Animator>().Play("GorillaCarry");
                carrying = true;
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
        else{
            Physics.IgnoreCollision(this.GetComponent<MeshCollider>(), GameObject.FindWithTag("Player").GetComponent<MeshCollider>(), true);
            Physics.IgnoreCollision(this.GetComponent<MeshCollider>(), GameObject.FindWithTag("Player").GetComponent<CharacterController>(), true);
        }
    }

    public void TeleportOut()
    {
        GameObject gorSphere = GameObject.Find("GorillaSphereCopy");
        Vector3 sphereLoc = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        GameObject sphere;
        sphere = Instantiate(gorSphere, sphereLoc, this.gameObject.transform.rotation);
        sphere.GetComponent<PrimateTeleport>().enabled = true;
    }

    IEnumerator BeginningWait()
    {
        yield return new WaitForSeconds(2f);
        startMoving = true;
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
        else if (other.tag == "Object" && other.gameObject == target && !holdingObject && canPickup)
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
                Explosion(other.gameObject);
                StartCoroutine("KnockBack", other.transform.position);
            }
        }
    }

    private void Explosion(GameObject nuke)
    {
        Instantiate(GameObject.Find("Explosion"), nuke.transform.position, this.gameObject.transform.rotation);
    }

    private void PickUpObject(Collider other)
    {
        fakeHolding = Instantiate(target, target.transform.position, target.transform.rotation);
        other.gameObject.transform.parent = this.transform;
        other.gameObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        //other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        fakeHolding.gameObject.transform.parent = this.transform;
        fakeHolding.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x - 3f, other.gameObject.transform.position.y + 1f, other.gameObject.transform.position.z + 2.0f);
        fakeHolding.gameObject.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        target.GetComponent<Rigidbody>().isKinematic = true;
        fakeHolding.GetComponent<Rigidbody>().isKinematic = true;
        holdingObject = true;
        canPickup = false;
        objectHeld = target;
        objectHeld.SetActive(false);
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
            this.GetComponent<Animator>().Play("GorillaIdle");
            
            //-- WINDUP ANIMATION --//
            this.GetComponent<Animator>().Play("GorillaWindUp");
            
            this.transform.LookAt(target.transform.position);
            this.GetComponent<Rigidbody>().ResetInertiaTensor(); // reset inertia before charging

            yield return new WaitForSeconds(1f); // time in seconds to wait
            
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 700f , ForceMode.Impulse);
            GameObject rocketLoc = GameObject.Find("RocketLoc");
            Instantiate(GameObject.Find("Rocket"), rocketLoc.transform.position, this.gameObject.transform.rotation);
            audioData.clip = clip2;
            audioData.PlayOneShot(clip2, 1.0f);
            audioData.PlayOneShot(clip3, 1.0f);

            yield return new WaitForSeconds(1f); // charge for 1 second
            
            this.GetComponent<Rigidbody>().velocity = Vector3.zero; // remove forces on gorilla so he stops
            this.GetComponent<Rigidbody>().isKinematic = true;
            charging = false;
            //agent.speed = 0; // stops
            //agent.isStopped = true;

            yield return new WaitForSeconds(0.5f); // gorilla self-stun after it charges

            StartGorilla();
            this.GetComponent<Animator>().Play("GorillaWalk2");
        
            yield return new WaitForSeconds(chargeCooldown); // charge cooldown
            canCharge = true;
        }
    }

    IEnumerator ThrowObject()
    {
        if (holdingObject && visibleTargets.Count != 0)
        {
            StopGorilla();
            this.gameObject.transform.LookAt(visibleTargets[0].transform.position);
            yield return new WaitForSeconds(0.5f); // wait for 1 second
            Destroy(fakeHolding);
            objectHeld.SetActive(true);
            objectHeld.GetComponent<Rigidbody>().isKinematic = false;
            objectHeld.tag = "ThrownObject";
            objectHeld.layer = 15;
            //Physics.IgnoreCollision(GetComponent<Collider>(), objectHeld.GetComponent<Collider>(), true);
            objectHeld.GetComponent<Rigidbody>().AddForce(this.transform.forward * 15f, ForceMode.Impulse);
            objectHeld.transform.parent = null;

            //-- THROW ANIMATION --//
            this.GetComponent<Animator>().Play("GorillaThrow");
            
            yield return new WaitForSeconds(1f); // wait for 1 second

            holdingObject = false;
            StartCoroutine("ThrowWait");
            StartCoroutine("DestroyThrown", objectHeld);
            StartGorilla();
            this.GetComponent<Animator>().Play("GorillaWalk1");
        }
    }

    IEnumerator ThrowWait()
    {
        yield return new WaitForSeconds(5f); // wait for 5 seconds
        canPickup = true;
    }

    IEnumerator DestroyThrown(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }

    // This coroutine handles part of the Gorilla/Player collision interaction.
    // If the Gorilla hits the player, he should wait for a little bit before moving again. 
    IEnumerator AttackPlayer(Player1P player){
        Debug.Log("ATTACK PLAYER");

        //-- ATTACK ANIMATION --//
        this.GetComponent<Animator>().Play("GorillaClap");
        audioData.clip = clip1;
        audioData.Play(0);

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
            player.StartCoroutine("updateHealth", true);
        }
            
        yield return new WaitForSeconds(0.75f); // wait
        
        StartGorilla();
        this.GetComponent<Animator>().Play("GorillaWalk1");
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
        this.GetComponent<Animator>().Play("GorillaIdle");
        
        this.GetComponent<Rigidbody>().velocity = Vector3.zero; // remove forces on gorilla so he stops
        this.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(2f); // banana stuns gorilla for 2 seconds
        
        StartGorilla();
        this.GetComponent<Animator>().Play("GorillaWalk2");
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
        this.GetComponent<Animator>().Play("GorillaIdle");
        
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
        this.GetComponent<Animator>().Play("GorillaWalk2");
        
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
