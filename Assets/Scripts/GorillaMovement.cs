//referenced using Single Sapling Games' video on "Stop Enemy When close to Player - FPS Game in Unity - Part 57" : https://www.youtube.com/watch?v=26Oavz7WTC0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorillaMovement : MonoBehaviour
{
	float stoppingDistance = 15f;
    float _SPEED = 12f;
    float _ACCELERATION = 8f;
    float _ANGULAR_SPEED = 120f;

	NavMeshAgent agent;

	GameObject target;
    GameObject node1;
    GameObject node2;
    GameObject node3;
    GameObject node4;
    GameObject node5;
    List<GameObject> nodes; 
    GameObject playerObj;
    GameObject targetNode;

    private bool canCharge = true;
    private bool charging = false;
    private bool playerLock = false;
    private float playerDist = 0f;
    FieldOfView targetsList;
    public List<Transform> visibleTargets = new List<Transform>();


    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nodes = new List<GameObject>();
        node1 = GameObject.FindGameObjectWithTag("ElecControl");
        nodes.Add(node1);
        node2 = GameObject.FindGameObjectWithTag("ElecControl2");
        nodes.Add(node2);
        node3 = GameObject.FindGameObjectWithTag("Nav");
        nodes.Add(node3);
        node4 = GameObject.FindGameObjectWithTag("Reactor");
        nodes.Add(node4);
        node5 = GameObject.FindGameObjectWithTag("O2");
        nodes.Add(node5);

        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerDist = Vector3.Distance (transform.position, playerObj.transform.position);

        
        //agent.speed = _SPEED;
        //agent.acceleration = _ACCELERATION;
        //agent.angularSpeed = _ANGULAR_SPEED;
        
        int targetnum = Random.Range(0, nodes.Count-1);
        targetNode = nodes[targetnum];
        target = targetNode;
        //Debug.Log(target);
    }


    // Update is called once per frame
    private void Update()    
    {
        //Get list of targets from FieldOfView list
        targetsList = GetComponent<FieldOfView>();
        //transfer each target into local list
        visibleTargets.Clear();
        foreach (Transform t in targetsList.visibleTargets)
        {
            visibleTargets.Add(t);
        }

        // check for nearby player - from what I can tell, 20-25 units is right on the edge of the player's screen
        playerDist = Vector3.Distance (transform.position, playerObj.transform.position);
        //Debug.Log("Player is " + playerDist + " units away");
            
        // if gorilla is not following player, check for the player distance
        if (target != playerObj){
            /*if (playerDist <= 30f){ // if player is close enough, set it as the target
                Debug.Log("Gorilla has locked on Player");
                stoppingDistance = 0; // make stopping distance 0 if tracking the player
                target = playerObj;
            }*/
            if (visibleTargets.Count != 0)
            {
                Debug.Log("Gorilla has locked on Player");
                playerLock = true;
                stoppingDistance = 0; // make stopping distance 0 if tracking the player
                target = playerObj;
            }
        }
        //else if (playerDist > 30f && !charging){ // if target is player but player is too far, ignore the player.
        //    Debug.Log("Gorilla is ignoring the Player");
        //    target = targetNode;
        //    stoppingDistance = 15f; // make stopping distance 15 if idle
        //}

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
        if (target == playerObj && canCharge && playerLock ){
            //Debug.Log("Gorilla is charging");
            StartCoroutine("ChargeAttack");
            canCharge = false;
        } 
        else if (!charging){
       	    agent.isStopped = false;
       	    agent.SetDestination(target.transform.position);
        }
    }

    private void StopEnemy()
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
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine("AttackPlayer");
        }
    }
    
    // this coroutine manages the Gorilla ChargeAttack.
    // It ends if the gorilla stops chasing the player.
    // If the gorilla can charge, it will wait for 1.5s, and charge for 1.5s, then self-stun for .5s.
    // charge cooldown is 5 seconds.
    IEnumerator ChargeAttack(){
        if (canCharge){ // some condition here to initiate the charge attack (maybe also consider player distance?)
            charging = true;
            agent.isStopped = true; // stop gorilla mvmt
            agent.speed = 0;
            agent.acceleration = 100;
            agent.angularSpeed = 15; // decrease the angular speed so it doesn't turn as much
            agent.autoBraking = false; // this lets the gorilla overshoot, so the mvmt is more realistic
            
            Vector3 chargePos = playerObj.transform.position; // set target to player position at this moment
            //this.transform.LookAt(playerObj.transform.position);
            this.transform.LookAt(chargePos);
            agent.SetDestination(chargePos);
            //Debug.Log(chargePos);
            yield return new WaitForSeconds(1f); // time in seconds to wait
            
            //Debug.Log(chargePos);
            agent.isStopped = false;
            agent.speed = 50;
            agent.SetDestination((chargePos + playerObj.transform.position)/2);
            yield return new WaitForSeconds(1f); // charge for 1 second
            
            charging = false;
            agent.speed = 0; // stops
            agent.isStopped = true;
            yield return new WaitForSeconds(1f); // gorilla self-stun after it charges
            
            agent.SetDestination(target.transform.position); // reset target
            agent.autoBraking = true;
            agent.speed = _SPEED;
            agent.angularSpeed = _ANGULAR_SPEED;
            agent.acceleration = _ACCELERATION;
            agent.isStopped = false;
            //stoppingDistance = 10f;
        
            yield return new WaitForSeconds(5f); // charge cooldown
            canCharge = true;
        }
    }

    // This coroutine handles part of the Gorilla/Player collision interaction.
    // If the Gorilla hits the player, he should wait for a little bit before moving again. 
    IEnumerator AttackPlayer(){
        agent.speed = 0;
        //animator.play("attack-anim"); // play the gorilla attack animation

        yield return new WaitForSeconds(1f); // wait one second
        agent.speed = _SPEED; // reset speed to initial value;
    }
    
}
