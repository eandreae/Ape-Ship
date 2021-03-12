using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class foodSpawner : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        currColor = nodeColor.text;
        canSpawn = true;
        foodItems = new GameObject[4];
        foodItems[0] = GameObject.Find("KrillSandwich");
        foodItems[1] = GameObject.Find("SeaFoodKebab");
        foodItems[2] = GameObject.Find("SodaNuke");
        foodItems[3] = GameObject.Find("Banana");
        //gorillaScript = GetComponent<GorillaMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //charging = gorillaScript.charging;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && canSpawn) // player can spawn items from vending machine on a cooldown
        {
            spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            if (nodeColor.text != "green")
            {
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
                canSpawn = false;
                StartCoroutine("SpawnTimer", 5.0f); // add 5 second cd to using vending machine
            }
        } else if(coll.gameObject.tag == "Gorilla" && coll.GetComponent<GorillaMovement>().charging) // gorilla collision when charging means spawn item no matter what
        {
            spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            if (nodeColor.text != "green")
            {
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
                canSpawn = false;
                StartCoroutine("SpawnTimer", 5.0f); // add 5 second cd to using vending machine
            }
        }
    }

    IEnumerator SpawnTimer(float cooldown){
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
