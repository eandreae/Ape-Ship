//referenced using Single Sapling Games' video on "Stop Enemy When close to Player - FPS Game in Unity - Part 57" : https://www.youtube.com/watch?v=26Oavz7WTC0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
	[SerializeField] float stoppingDistance;

	NavMeshAgent agent;

	GameObject target;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
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
    }
    
}
