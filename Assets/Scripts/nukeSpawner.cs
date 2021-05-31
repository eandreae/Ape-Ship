using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class nukeSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    public GameObject[] foodItems;
    public Text nodeColor;
    Vector3 spawnLoc;
    //GorillaMovement gorillaScript;
    private bool canSpawn;
    private Animator foodAnim;
    private GameObject vendingMachine;
    private NetworkManager nm;

    // Start is called before the first frame update
    void Start()
    {
        nm = GameObject.FindObjectOfType<NetworkManager>();
        if (nm)
        {
            Object.Destroy(this.gameObject);
        }

        canSpawn = true;

        spawnee = GameObject.Find("SodaNuke (1P)"); // get nuke
        //gorillaScript = GetComponent<GorillaMovement>();

        vendingMachine = GameObject.Find("NukeVendingMachine");
        //foodAnim = vendingMachine.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //charging = gorillaScript.charging;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && canSpawn) // player can spawn items from vending machine on a cooldown
        {
            //spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            //Changed to always spawn
            //if (nodeColor.text != "green")
            //{
            //foodAnim.Play("PushButton");
            GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
            temp.GetComponent<Rigidbody>().useGravity = true;
            temp.GetComponent<destroyer>().enabled = true;
            canSpawn = false;
            StartCoroutine("SpawnTimer", 10.0f); // add 5 second cd to using vending machine
            //}
        }
        else if (coll.gameObject.tag == "Gorilla" && coll.GetComponent<Gorilla1P>().charging) // gorilla collision when charging means spawn item no matter what
        {
            //spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            //foodAnim.Play("PushButton");
            GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
            temp.GetComponent<Rigidbody>().useGravity = true;
            temp.GetComponent<destroyer>().enabled = true;
            canSpawn = false;
            StartCoroutine("SpawnTimer", 2.0f); // add 2 second cd to using vending machine
            //}
        }
    }

    IEnumerator SpawnTimer(float cooldown)
    {
        spawnee = GameObject.Find("SodaNuke (1P)"); // get nuke
        Debug.Log(spawnee);
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }

}
