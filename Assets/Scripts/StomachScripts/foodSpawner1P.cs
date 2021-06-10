using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class foodSpawner1P : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    public GameObject[] foodItems;
    string currColor;
    public Text nodeColor;
    Vector3 spawnLoc;
    //GorillaMovement gorillaScript;
    private bool charging;
    private bool canSpawn;
    private Animator foodAnim;
    private GameObject vendingMachine;
    private NetworkManager nm; 
    private bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {   
        nm = GameObject.FindObjectOfType<NetworkManager>();
        if (nm){
            Object.Destroy(this.gameObject);
        }

        currColor = nodeColor.text;
        canSpawn = true;

        spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn

        vendingMachine = GameObject.Find("TexturedVendingMachine");
        foodAnim = vendingMachine.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && canSpawn && playerInRange) {
            SpawnFood(5.0f);
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player") {
            playerInRange = true;
            this.GetComponent<HighlightScript>().highlightOn();
        }
        if(coll.gameObject.tag == "Gorilla" && coll.GetComponent<Gorilla1P>().charging) // gorilla collision when charging means spawn item no matter what
        {
            SpawnFood(2.0f);
        }
    }

    private void SpawnFood(float cooldown)
    {
        spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
        foodAnim.Play("PushButton");
        currColor = nodeColor.text;
        
        GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
        temp.GetComponent<Rigidbody>().useGravity = true;
        temp.GetComponent<destroyer>().enabled = true;
        canSpawn = false;
        StartCoroutine("SpawnTimer", cooldown);
    }

    private void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "Player") {
            playerInRange = false;
            this.GetComponent<HighlightScript>().highlightOff();
        }
    }

    IEnumerator SpawnTimer(float cooldown){
        spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
        Debug.Log(spawnee);
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }

    /*private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Gorilla" && charging)
        {
            Debug.Log("WHOOOO");

            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            if (nodeColor.text != currColor)
            {
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
            }

        }
    }*/

}
