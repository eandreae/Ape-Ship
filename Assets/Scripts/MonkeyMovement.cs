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
    static readonly string[] nodeTags = {
        "Stomach",
        "ElecControl",
        "Nav",
        "Reactor",
        "O2",
        };
    List<GameObject> nodes;
    GameObject target;
    Text targetColor;
    FieldOfView targetsList;
    public List<Transform> visibleTargets = new List<Transform>();

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        nodes = new List<GameObject>();
        foreach (string name in nodeTags) {
            nodes.Add(GameObject.FindGameObjectWithTag(name));
        }

        FindNewTarget();
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(agent.acceleration);
        //Debug.Log(agent.speed);
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

            // code for avoiding player
            //agent.isStopped = true;
            //Debug.Log(visibleTargets[0]);
            Vector3 targetDir = this.transform.position - visibleTargets[0].position; // with multiple players, maybe take the sum of the positions?
            //Debug.Log(targetDir);
            //agent.SetDestination(this.transform.position + targetDir);
        }
        else { // return to normal behavior
            agent.acceleration = 10; 
            //float dist = Vector3.Distance(transform.position, target.transform.position);
            if (targetColor.text == "red")
            {
                FindNewTarget();
            }
            else
            {
                GoToTarget();
            }
        }
    }

    private void FindNewTarget()
    {
        // Find nearest node, disqualifying the current target
        int targetIndex = -1;
        float minDist = 99999;
        for (int i = 0; i < nodes.Count; i++) {
            // If the node is not the target
            if (nodes[i] != target) {
                float dist = Vector3.Distance (transform.position, nodes[i].transform.position);
                if (dist < minDist) {
                    // Set target color to check if it is red
                    SetTargetColor(i);
                    if (targetColor.text != "red")
                    {
                        targetIndex = i;
                        minDist = dist;
                    }
                }
            }
        }

        // Reset target color
        SetTargetColor(targetIndex);
        
        //target = GameObject.FindGameObjectWithTag("Nav");
        target = nodes[targetIndex];
        //target = nodes[Random.Range(0, nodes.Count)];
        Debug.Log("Monkey moving to:" + target);
    }
    private void SetTargetColor(int index)
    {
        switch (index+1) {
            case 1: 
                targetColor = color1;
                break;
            case 2: 
                targetColor = color2;
                break;
            case 3: 
                targetColor = color3;
                break;
            case 4: 
                targetColor = color4;
                break;
            case 5: 
                targetColor = color5;
                break;
            default:
                break;
        }
    }

    private void GoToTarget()
    {
        //agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    private void StopEnemy()
    {
        agent.isStopped = true;
    }

}