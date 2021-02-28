using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class NodeInstanceManager : MonoBehaviour
{
    [SerializeField] private Renderer myObject; 

    public UnityEngine.Color color;

    public Text colorTracker;
    public Image display;
    public float stopDistance;
    private float playerDist;
    private float monkeyDist;
    GameObject playerObj;
    GameObject monkeyObj;
    NavMeshAgent agent;
    public bool canHack = true; // can be hacked by monkey
    //private MonkeyMovement flee;
    private bool isFleeing;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        monkeyObj = GameObject.FindGameObjectWithTag("Monkey");
        agent = monkeyObj.GetComponent<NavMeshAgent>();

        UpdateColor();

    }


    private void Update()
    {
        //Debug.Log(transform.position);
        playerDist = Vector3.Distance(transform.position, playerObj.transform.position);
        monkeyDist = Vector3.Distance(transform.position, monkeyObj.transform.position);
        //flee = GetComponent<MonkeyMovement>();
        isFleeing = MonkeyMovement.runningAway;
        Debug.Log(isFleeing);
        //Change color to match text color
        UpdateColor();

        //Debug.Log(playerObj.transform.position);
        //Debug.Log(transform.position);
        //TEMPORARY
        //player turns every node immediately green
        if (playerDist < stopDistance)
        {
            //temporary until all minigames are implemented
            if(gameObject.tag != "Nav" && gameObject.tag != "Stomach")
            {
                myObject.material.color = Color.green;
                display.color = Color.green;
                colorTracker.text = "green";
            }
        }
        //TEMPORARY
        //monkey turns every node down one level
        if (monkeyDist < stopDistance && canHack && !isFleeing)
        {
            StartCoroutine("destroyNode");
            StartCoroutine("HackCD", 5.0f); // use 5s cooldown
        }
        //Stop Coroutines if monkey starts fleeing
        if (isFleeing)
        {
            StopCoroutine("destoryNode");
            agent.isStopped = false;
            agent.speed = 20;
            agent.acceleration = 10;
        }
    }

    private void UpdateColor()
    {
        if (colorTracker.text == "green")
        {
            myObject.material.color = Color.green;
            display.color = Color.green;
        }
        else if (colorTracker.text == "yellow")
        {
            myObject.material.color = Color.yellow;
            display.color = Color.yellow;
        }
        else if (colorTracker.text == "red")
        {
            myObject.material.color = Color.red;
            display.color = Color.red;
        }
    }

    public void SetColor(UnityEngine.Color input)
    {
        color = input;
        myObject.material.color = input;
        display.color = input;
    }

    public void DamageNode()
    {
        if (color == Color.yellow)
        {
            SetColor(Color.red);
            colorTracker.text = "red";
        }
        else if (color == Color.green)
        {
            SetColor(Color.yellow);
            colorTracker.text = "yellow";
        }
    }

    public void FixNode()
    {
        if (color == Color.yellow)
        {
            SetColor(Color.green);
            colorTracker.text = "green";
        }
        else if (color == Color.red)
        {
            SetColor(Color.yellow);
            colorTracker.text = "yellow";
        }
    }

    IEnumerator destroyNode()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(3f); // time in seconds to wait
        agent.isStopped = false;
        agent.speed = 20;
        agent.acceleration = 10;
        DamageNode();
    }
    IEnumerator HackCD (float cooldown = 10.0f) { // default is 10s
        if (canHack){
            canHack = false;
            yield return new WaitForSeconds(cooldown);
            canHack = true;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(color.text == "yellow")
            {
                myObject.material.color = Color.green;
                display.color = Color.green;
                color.text = "green";
            } else if(color.text == "red")
            {
                myObject.material.color = Color.yellow;
                display.color = Color.yellow;
                color.text = "yellow";
            }
        } else if(other.CompareTag("Monkey"))
        {
            if (color.text == "yellow")
            {
                myObject.material.color = Color.red;
                display.color = Color.red;
                color.text = "red";
            } else if(color.text == "green")
            {
                myObject.material.color = Color.red;
                display.color = Color.red;
                color.text = "red";
            }
        }
    }*/
}
