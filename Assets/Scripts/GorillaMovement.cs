//referenced using Single Sapling Games' video on "Stop Enemy When close to Player - FPS Game in Unity - Part 57" : https://www.youtube.com/watch?v=26Oavz7WTC0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorillaMovement : MonoBehaviour
{
	float stoppingDistance = 10f;
    float _SPEED = 12f;
    float _ACCELERATION = 8f;

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
    private float playerDist = 0f;
    
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

        _SPEED = agent.speed;
        _ACCELERATION = agent.acceleration;

        int targetnum = Random.Range(0, 4);
        targetNode = nodes[targetnum];
        target = targetNode;
    }

    // Update is called once per frame
    private void Update()    
    {
        // check for nearby player - from what I can tell, 20-25 units is right on the edge of the player's screen
        playerDist = Vector3.Distance (transform.position, playerObj.transform.position);
        //Debug.Log("Player is " + playerDist + " units away");
            
        // if gorilla is not following player, check for the player distance
        if (target != playerObj){        
            if (playerDist <= 30f){ // if player is close enough, set it as the target
                Debug.Log("Gorilla has locked on Player");
                target = playerObj;
            }
        }
        else if (playerDist > 30f && !charging){
            target = targetNode;
        }

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist < stoppingDistance)
        {
        	StopEnemy();
        }
        else 
        {
        	GoToTarget();
        }
    }

        
    private void GoToTarget()
    {
         // If gorilla is CHASING PLAYER, HAS CHARGE CD, and is CLOSE TO PLAYER, it will charge
        if (target == playerObj && canCharge && playerDist <= 20f){
            StartCoroutine("ChargeAttack");
            canCharge = false;
        } else{
       	    agent.isStopped = false;
       	    agent.SetDestination(target.transform.position);
        }
    }

    private void StopEnemy()
    {
      	agent.isStopped = true;
        //get new target
        //Start();
        int targetnum = Random.Range(0, 4);
        targetNode = nodes[targetnum];
        target = targetNode;
    }
    
    // this coroutine manages the Gorilla ChargeAttack.
    // It ends if the gorilla stops chasing the player.
    // If the gorilla can charge, it will wait for 1.5s, and charge for 1s, then self-stun for 1s.
    // charge cooldown is 5 seconds.
    IEnumerator ChargeAttack(){
        Debug.Log("preparing to charge");
        if (canCharge){ // some condition here to initiate the charge attack (maybe also consider player distance?)
            charging = true;
            agent.isStopped = true; // stop gorilla mvmt
            agent.speed = 0;
            agent.acceleration = 100;
            agent.autoBraking = false; // this lets the gorilla overshoot, so the mvmt is more realistic
            stoppingDistance = 0;
            //Debug.Log("Gorilla STOP");

            Vector3 chargePos = playerObj.transform.position; // set target to player position at this moment
            agent.SetDestination(chargePos);
            yield return new WaitForSeconds(1.5f); // time in seconds to wait
            
            agent.speed = 40;
            yield return new WaitForSeconds(1f); // charge for 1.5 second
            
            charging = false;
            agent.speed = 0; // stops
            yield return new WaitForSeconds(1f); // gorilla self-stun after it charges

            agent.autoBraking = true;
            agent.SetDestination(target.transform.position); // reset target
            agent.speed = _SPEED;
            agent.acceleration = _ACCELERATION;
            agent.isStopped = false;
            stoppingDistance = 10f;
        
            yield return new WaitForSeconds(5f); // charge cooldown
            canCharge = true;
        }
    }
}
