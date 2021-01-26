using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonkeyMovement : MonoBehaviour
{
    public float stoppingDistance  = 1.0f;
    public Text color1;
    public Text color2;
    public Text color3;
    public Text color4;
    public Text color5;

    NavMeshAgent agent;

    GameObject target;
    GameObject node1;
    GameObject node2;
    GameObject node3;
    GameObject node4;
    GameObject node5;
    //List <GameObject> nodes;
    Text targetColor;
    FieldOfView targetsList;
    public List<Transform> visibleTargets = new List<Transform>();

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        List<GameObject> nodes = new List<GameObject>();
        //nodes = new List<GameObject>();
        List<float> distances = new List<float>();
        node1 = GameObject.FindGameObjectWithTag("ElecControl");
        node2 = GameObject.FindGameObjectWithTag("ElecControl2");
        node3 = GameObject.FindGameObjectWithTag("Nav");
        node4 = GameObject.FindGameObjectWithTag("Reactor");
        node5 = GameObject.FindGameObjectWithTag("O2");

        if (color1.text == "green" || color1.text == "yellow")
        {
            nodes.Add(node1);
            distances.Add(Vector3.Distance(transform.position, node1.transform.position));
        }
        if (color2.text == "green" || color2.text == "yellow")
        {
            nodes.Add(node2);
            distances.Add(Vector3.Distance(transform.position, node2.transform.position));
        }
        if (color3.text == "green" || color3.text == "yellow")
        {
            nodes.Add(node3);
            distances.Add(Vector3.Distance(transform.position, node3.transform.position));
        }
        if (color4.text == "green" || color4.text == "yellow")
        {
            nodes.Add(node4);
            distances.Add(Vector3.Distance(transform.position, node4.transform.position));
        }
        if (color5.text == "green" || color5.text == "yellow")
        {
            nodes.Add(node5);
            distances.Add(Vector3.Distance(transform.position, node5.transform.position));
        }
        //nodes.Add(node1);
        //nodes.Add(node2);
        //nodes.Add(node3);
        //nodes.Add(node4);
        //nodes.Add(node5);

        float min = 99999;
        int index = 0;
        for(int i = 0; i < nodes.Count; ++i)
        {
            if(distances[i] < min)
            {
                min = distances[i];
                index = i;
            }
        }

        
        if(nodes[index] == node1)
        {
            targetColor = color1;
        } else if(nodes[index] == node2)
        {
            targetColor = color2;
        }
        else if(nodes[index] == node3)
        {
            targetColor = color3;
        }
        else if(nodes[index] == node4)
        {
            targetColor = color4;
        }
        else if(nodes[index] == node5)
        {
            targetColor = color5;
        }


        //target = GameObject.FindGameObjectWithTag("Nav");
        target = nodes[index];
        //target = nodes[Random.Range(0, nodes.Count)];
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
        //Speed up if player is seen
        if(visibleTargets.Count != 0)
        {
            agent.acceleration = 50;
        } else
        {
            agent.acceleration = 10;
        }

        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (targetColor.text == "red")
        {
            Start();
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