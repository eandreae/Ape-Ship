//referenced using Single Sapling Games' video on "Stop Enemy When close to Player - FPS Game in Unity - Part 57" : https://www.youtube.com/watch?v=26Oavz7WTC0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorillaMovement : MonoBehaviour
{
	float stoppingDistance = 10f;

	NavMeshAgent agent;

	GameObject target;
    GameObject node1;
    GameObject node2;
    GameObject node3;
    GameObject node4;
    GameObject node5;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        List<GameObject> nodes = new List<GameObject>();
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

        int targetnum = Random.Range(0, 4);
        target = nodes[targetnum];
    }

    // Update is called once per frame
    private void Update()    
    {
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
       	agent.isStopped = false;
       	agent.SetDestination(target.transform.position);
    }

    private void StopEnemy()
    {
      	agent.isStopped = true;
        //get new target
        Start();
    }
    
}
