using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class nukeSpawner1P : NetworkBehaviour
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
    private bool inrange = false;

    // Start is called before the first frame update
    void Start()
    {
        nm = GameObject.FindObjectOfType<NetworkManager>();
        if(nm)
            Object.Destroy(this.gameObject);
            
        canSpawn = true;

        spawnee = GameObject.Find("SodaNuke (1P)"); // get nuke
        //gorillaScript = GetComponent<GorillaMovement>();

        vendingMachine = GameObject.Find("NukeVendingMachine");
        //foodAnim = vendingMachine.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && inrange && canSpawn)
        {
           SpawnItem(5.0f);
        }
        //charging = gorillaScript.charging;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player") // player can spawn items from vending machine on a cooldown
        {
            inrange = true;
            this.GetComponent<HighlightScript>().highlightOn();
        }
        else if (coll.gameObject.tag == "Gorilla" && coll.GetComponent<Gorilla1P>().charging && canSpawn) // gorilla collision when charging means spawn item no matter what
        {
            SpawnItem(2.0f);
        }
    }

    private void SpawnItem(float cooldown)
    {
        spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));

        GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
        temp.GetComponent<Rigidbody>().useGravity = true;
        temp.GetComponent<destroyer>().enabled = true;
        canSpawn = false;
        StartCoroutine("SpawnTimer", cooldown);
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            inrange = false;
            this.GetComponent<HighlightScript>().highlightOff();
        }
    }

    IEnumerator SpawnTimer(float cooldown)
    {
        spawnee = GameObject.Find("SodaNuke (1P)"); // get nuke
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }

}
