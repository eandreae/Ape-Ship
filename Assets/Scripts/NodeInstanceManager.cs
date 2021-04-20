using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;

public class NodeInstanceManager : MonoBehaviour
{
    [SerializeField] private Renderer myObject; 

    public UnityEngine.Color color;
    public UnityEvent OnNodeFix;
    public UnityEvent OnNodeDamage;

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
        
        // Need to set starting color for each node

        UpdateColor();

    }


    private void Update()
    {
        //Debug.Log(transform.position);
        playerDist = Vector3.Distance(transform.position, playerObj.transform.position);
        monkeyDist = Vector3.Distance(transform.position, monkeyObj.transform.position);
        //flee = GetComponent<MonkeyMovement>();
        isFleeing = MonkeyMovement.runningAway;
        //Debug.Log(isFleeing);
        //Change color to match text color
        UpdateColor();

        //TEMPORARY
        //player turns every node immediately green
        if (playerDist < stopDistance)
        {
            //temporary until all minigames are implemented
            if(gameObject.tag != "Nav" && gameObject.tag != "Stomach" && gameObject.tag != "O2_2" && gameObject.tag != "O2" && gameObject.tag != "Reactor")
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
            StopCoroutine("destroyNode");
            //Debug.Log("Monkey stop punching Node");
            monkeyObj.GetComponent<Animator>().Play("walk");
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
            color = Color.green;
        }
        else if (colorTracker.text == "yellow")
        {
            myObject.material.color = Color.yellow;
            display.color = Color.yellow;
            color = Color.yellow;
        }
        else if (colorTracker.text == "red")
        {
            myObject.material.color = Color.red;
            display.color = Color.red;
            color = Color.red;
        }
    }

    public void SetColor(UnityEngine.Color input)
    {
        color = input;
        myObject.material.color = input;
        display.color = input;
    }

    public UnityEngine.Color GetColor()
    {
        return color;
    }

    public void DamageNode()
    {
        Debug.Log("DamageNode");
        if (color == Color.yellow)
        {
            SetColor(Color.red);
            colorTracker.text = "red";
            OnNodeDamage.Invoke();
        }
        else if (color == Color.green)
        {
            Debug.Log("DamageNodeGreen-Yellow");
            SetColor(Color.yellow);
            colorTracker.text = "yellow";
            OnNodeDamage.Invoke();
        }
    }

    public void FixNode()
    {
        if (color == Color.yellow)
        {
            SetColor(Color.green);
            colorTracker.text = "green";
            OnNodeFix.Invoke();
        }
        else if (color == Color.red)
        {
            SetColor(Color.yellow);
            colorTracker.text = "yellow";
            OnNodeFix.Invoke();
        }
    }

    IEnumerator destroyNode()
    {
        //Debug.Log("Monkey punch Node");
        monkeyObj.GetComponent<Animator>().Play("punch");
        agent.isStopped = true;
        yield return new WaitForSeconds(3f); // time in seconds to wait
        //Debug.Log("Monkey stop punching Node");
        monkeyObj.GetComponent<Animator>().Play("walk");
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
