using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Mirror;

public class MonkeyMovement : NetworkBehaviour
{
    public float stoppingDistance  = 12.0f;

    public Text color1;
    public Text color2;
    public Text color3;
    public Text color4;
    public Text color5;
    public Text color6;

    NavMeshAgent agent;
    static readonly string[] nodeTags = {
        "Stomach",
        "ElecControl",
        "Nav",
        "Reactor",
        "O2",
        "O2_2"
    };

    List<GameObject> nodes;
    public GameObject target;
    Text targetColor;
    FieldOfView targetsList;
    public List<Transform> visibleTargets = new List<Transform>();
    public static bool runningAway;
    private bool gotAway = true;
    GameObject playerObj;

    private bool startMoving;

    private float startval = -0.5f;
    private float endval = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
        runningAway = false;
        agent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.FindGameObjectWithTag("Player");

        nodes = new List<GameObject>();
        foreach (string name in nodeTags) {
            nodes.Add(GameObject.FindGameObjectWithTag(name));
        }

        StartCoroutine("BeginningWait");
    }

    // Update is called once per frame
    private void Update()
    {
        if (startMoving)
        {
            //Debug.Log(agent.acceleration);
            //Debug.Log(agent.speed);
            //Get list of targets from FieldOfView list
            targetsList = GetComponent<FieldOfView>();
            float monkeyDist = Vector3.Distance(transform.position, target.transform.position);
            //transfer each target into local list
            visibleTargets.Clear();
            foreach (Transform t in targetsList.visibleTargets)
            {
                visibleTargets.Add(t);
            }
            //Speed up if player is seen
            if(visibleTargets.Count != 0)
            {
                runningAway = true;
                agent.acceleration = 50;
                //Runs to new target farthest away from player
                gotAway = RunAway(gotAway);
                GoToTarget();
                // code for avoiding player
                //agent.isStopped = true;
                //Debug.Log(visibleTargets[0]);
                Vector3 targetDir = this.transform.position - visibleTargets[0].position; // with multiple players, maybe take the sum of the positions?
                //Debug.Log(targetDir);
                //agent.SetDestination(this.transform.position + targetDir);
            }
            else { // return to normal behavior
                agent.acceleration = 10;
                //Debug.Log(runningAway + " " + (monkeyDist < stoppingDistance));
                //float dist = Vector3.Distance(transform.position, target.transform.position);
                if (targetColor.text == "red" && !runningAway)
                {
                    FindNewTarget();
                }
                else if (runningAway == true && (monkeyDist < stoppingDistance))
                {
                    runningAway = false;
                    gotAway = true;
                    FindNewTarget();
                }
                else
                {
                    GoToTarget();
                }
            }
        }
    }

    public void TeleportOut()
    {
        GameObject monkSphere = GameObject.Find("MonkeySphereCopy");
        Vector3 sphereLoc = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2.5f, this.gameObject.transform.position.z);
        GameObject sphere;
        sphere = Instantiate(monkSphere, sphereLoc, this.gameObject.transform.rotation);
        sphere.GetComponent<PrimateTeleport>().enabled = true;
        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    IEnumerator BeginningWait()
    {
        yield return new WaitForSeconds(6f);
        startMoving = true;
        FindNewTarget();
    }


    [Server]
    private void FindNewTarget()
    {
        // Find nearest node, disqualifying the current target
        int targetIndex = -1;
        float minDist = 99999;
        for (int i = 0; i < nodes.Count; i++) {
            // If the node is not the target
            if (nodes[i] != target && nodes[i] != null) {
                float dist = Vector3.Distance(transform.position, nodes[i].transform.position);
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

    private bool RunAway(bool gotAway)
    {
        if (gotAway)
        {
            // Find farthest node away from the player
            int targetIndex = -1;
            float maxDist = -1000;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] != target && nodes[i] != null)
                {
                    float dist = Vector3.Distance(playerObj.transform.position, nodes[i].transform.position);
                    if (dist > maxDist)
                    {
                        targetIndex = i;
                        maxDist = dist;
                    }
                }
            }
            // Reset target color
            SetTargetColor(targetIndex);

            //target = GameObject.FindGameObjectWithTag("Nav");
            target = nodes[targetIndex];
            //target = nodes[Random.Range(0, nodes.Count)];
            Debug.Log("Monkey running to:" + target);
            gotAway = false;
        }
        return gotAway;
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
            case 6:
                targetColor = color6;
                break;
            default:
                break;
        }
    }

    [Server]
    private void GoToTarget()
    {
        //agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    public void StopEnemy()
    {
        agent.isStopped = true;
    }

}