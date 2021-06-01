using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using Mirror;

public class NodeInstanceManager : MonoBehaviour
{
    [SerializeField] private Renderer myObject; 
    public NetworkManager nm;
    public UnityEngine.Color color;
    public UnityEvent OnNodeFix;
    public UnityEvent OnNodeRedToYellow;
    public UnityEvent OnNodeGreen;
    public UnityEvent OnNodeDamage;
    public UnityEvent OnNodeGreenToYellow;
    public UnityEvent OnNodeRed;

    public Text colorTracker;
    public Image display;
    Animator displayAnim;
    public float stopDistance;
    private float playerDist;
    public float monkeyDist;
    GameObject playerObj;
    GameObject monkeyObj;
    NavMeshAgent agent;
    public bool canHack = true; // can be hacked by monkey
    //private MonkeyMovement flee;
    public bool isFleeing;
    [HideInInspector]
    public static float monkCooldown = 3f;
    static bool greyed = false;

    AudioSource nodeDisabledSFX;
    AudioSource nodeRepairedSFX;

    public float monkeySpawnTime = 4.3f;

    private void Start()
    {
        nm = GameObject.FindObjectOfType<NetworkManager>();

        if(nm){
            if (this.gameObject.name == "LungTarget (1P)")
                Object.Destroy(this.gameObject);
            if (this.gameObject.name == "Lungs_2Target (1P)")
                Object.Destroy(this.gameObject);
        }
        playerObj = GameObject.FindGameObjectWithTag("Player");
        monkeyObj = GameObject.FindGameObjectWithTag("Monkey");
        displayAnim = display.GetComponent<Animator>();
        agent = monkeyObj.GetComponent<NavMeshAgent>();
        
        // Need to set starting color for each node

        UpdateColor();

        Invoke("CheckForMonkeyAgain", monkeySpawnTime);
    }

    private void Awake()
    {
        nodeDisabledSFX = GetComponent<AudioSource>();
        nodeRepairedSFX = FindObjectOfType<GameManager>().GetComponent<AudioSource>();
    }

    void CheckForMonkeyAgain()
    {
        monkeyObj = GameObject.FindGameObjectWithTag("Monkey");
        agent = monkeyObj.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
         // temporary fix -- maybe use a list of all players for player distance?  maybe use a ontriggerenter? 
        if (!playerObj)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }   
        if (!monkeyObj)
        {
            monkeyObj = GameObject.FindGameObjectWithTag("Monkey");
            agent = monkeyObj.GetComponent<NavMeshAgent>();
        }

        //Debug.Log(transform.position);
        playerDist = Vector3.Distance(transform.position, playerObj.transform.position);
        monkeyDist = Vector3.Distance(transform.position, monkeyObj.transform.position);
        //flee = GetComponent<MonkeyMovement>();

        if(GameObject.FindObjectOfType<NetworkManager>()){
            isFleeing = MonkeyMovement.runningAway;
        } else {
            isFleeing = Monkey1P.runningAway;
        }

        //Debug.Log(isFleeing);
        if (this.gameObject.tag == "ElecControl")
        {
            if (colorTracker.text == "red")
            {
                greyed = true;
            }
            else
            {
                greyed = false;
            }
        }

        //Change color to match text color
        UpdateColor();

        //TEMPORARY
        //player turns every node immediately green
        if (playerDist < stopDistance)
        {
            /*//temporary until all minigames are implemented
            if(gameObject.tag != "Nav" && gameObject.tag != "Stomach" && gameObject.tag != "O2_2" && gameObject.tag != "O2" && gameObject.tag != "Reactor" && gameObject.tag != "ElecControl")
            {
                myObject.material.color = Color.green;
                display.color = Color.green;
                colorTracker.text = "green";
            }*/
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
        if (greyed == true && this.gameObject.tag != "ElecControl")
        {
            myObject.material.color = Color.grey;
            display.color = Color.grey;
        }
        else
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
                displayAnim.Play("MinimapYellowTask");
                color = Color.yellow;
            }
            else if (colorTracker.text == "red")
            {
                myObject.material.color = Color.red;
                display.color = Color.red;
                displayAnim.Play("MinimapRedTask");
                color = Color.red;
            }
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
        if (color == Color.yellow)
        {
            nodeDisabledSFX.Play();
            SetColor(Color.red);
            colorTracker.text = "red";
            OnNodeDamage.Invoke();
            OnNodeRed.Invoke();
        }
        else if (color == Color.green)
        {
            SetColor(Color.yellow);
            colorTracker.text = "yellow";
            OnNodeDamage.Invoke();
            OnNodeGreenToYellow.Invoke();
        }
    }

    public void FixNode()
    {
        if (color == Color.yellow)
        {
            SetColor(Color.green);
            colorTracker.text = "green";
            OnNodeFix.Invoke();
            OnNodeGreen.Invoke();
            nodeRepairedSFX.Play();
        }
        else if (color == Color.red)
        {
            SetColor(Color.yellow);
            colorTracker.text = "yellow";
            OnNodeFix.Invoke();
            OnNodeRedToYellow.Invoke();
            nodeRepairedSFX.Play();
        }
    }

    IEnumerator destroyNode()
    {
        //Debug.Log("Monkey punch Node");
        monkeyObj.GetComponent<Animator>().Play("punch");
        agent.isStopped = true;
        yield return new WaitForSeconds(monkCooldown); // time in seconds to wait
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
