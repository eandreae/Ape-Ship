using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ElecChangeInstance : MonoBehaviour
{
    [SerializeField] private Renderer myObject;

    public Text color;
    public Image display;
    public float stopDistance;
    private float playerDist;
    private float monkeyDist;
    GameObject playerObj;
    GameObject monkeyObj;
    NavMeshAgent agent;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        monkeyObj = GameObject.FindGameObjectWithTag("Monkey");
        agent = monkeyObj.GetComponent<NavMeshAgent>();

        if (color.text == "green")
        {
            myObject.material.color = Color.green;
            display.color = Color.green;
        } else if(color.text == "yellow")
        {
            myObject.material.color = Color.yellow;
            display.color = Color.yellow;
        } else if (color.text == "red")
        {
            myObject.material.color = Color.red;
            display.color = Color.red;
        }
    }


    private void Update()
    {
        //Debug.Log(transform.position);
        playerDist = Vector3.Distance(transform.position, playerObj.transform.position);
        monkeyDist = Vector3.Distance(transform.position, monkeyObj.transform.position);
        //Debug.Log(playerObj.transform.position);
        //Debug.Log(transform.position);
        //TEMPORARY
        //player turns every node immediately green
        if (playerDist < stopDistance)
        {
            if (color.text == "yellow")
            {
                myObject.material.color = Color.green;
                display.color = Color.green;
                color.text = "green";
            }
            else if (color.text == "green")
            {
                myObject.material.color = Color.green;
                display.color = Color.green;
                color.text = "green";
            } else if (color.text == "red")
            {
                myObject.material.color = Color.green;
                display.color = Color.green;
                color.text = "green";
            }
        }
        //TEMPORARY
        //monkey turns every node down one level
        if (monkeyDist < stopDistance)
        {
            StartCoroutine("destroyNode");
        }
    }

    IEnumerator destroyNode()
    {
        agent.speed = 0;
        agent.acceleration = 0;
        yield return new WaitForSeconds(3f); // time in seconds to wait
        agent.speed = 20;
        agent.acceleration = 10;
        if (color.text == "yellow")
        {
            myObject.material.color = Color.red;
            display.color = Color.red;
            color.text = "red";
        }
        else if (color.text == "green")
        {
            myObject.material.color = Color.yellow;
            display.color = Color.yellow;
            color.text = "yellow";
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
