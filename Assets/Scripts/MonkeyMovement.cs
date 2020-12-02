using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyMovement : MonoBehaviour
{
    [SerializeField] float stoppingDistance;

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
        /*
        node1 = GameObject.FindGameObjectWithTag("ElecControl");
        node2 = GameObject.FindGameObjectWithTag("ElecControl2
        */
        node3 = GameObject.FindGameObjectWithTag("Nav");
        /*
        node4 = GameObject.FindGameObjectWithTag("Reactor");
        node5 = GameObject.FindGameObjectWithTag("O2");
        */
        List<float> distances = new List<float>();
        /*
        distances.Add(Vector3.Distance(transform.position, node1.transform.position));
        distances.Add(Vector3.Distance(transform.position, node2.transform.position));
        */
        distances.Add(Vector3.Distance(transform.position, node3.transform.position));
        Debug.Log(Vector3.Distance(transform.position, node3.transform.position));
        /*
        distances.Add(Vector3.Distance(transform.position, node4.transform.position));
        distances.Add(Vector3.Distance(transform.position, node5.transform.position));

        float min = 99999;
        int index = 0;
        for(int i = 0; i < 5; ++i)
        {
            if(distances[i] < min)
            {
                min = distances[i];
                index = i;
            }
        }
        Debug.Log(min);
        Debug.Log(index);




        if (index == 0)
        {
            target = GameObject.FindGameObjectWithTag("ElecControl");
        } else if (index == 1)
        {
            Debug.Log("HERE");
            target = GameObject.FindGameObjectWithTag("ElecControl2");
        } else if (index == 2)
        {
            target = GameObject.FindGameObjectWithTag("Nav");
        } else if (index == 3)
        {
            target = GameObject.FindGameObjectWithTag("Reactor");
        } else
        {
            target = GameObject.FindGameObjectWithTag("O2");

        }*/
        target = GameObject.FindGameObjectWithTag("Nav");
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