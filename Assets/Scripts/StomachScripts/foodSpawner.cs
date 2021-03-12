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

    // Start is called before the first frame update
    void Start()
    {
        currColor = nodeColor.text;
        foodItems = new GameObject[4];
        foodItems[0] = GameObject.Find("KrillSandwich");
        foodItems[1] = GameObject.Find("SeaFoodKebab");
        foodItems[2] = GameObject.Find("SodaNuke");
        foodItems[3] = GameObject.Find("Banana");
        Debug.Log(foodItems.Length);
        //gorillaScript = GetComponent<GorillaMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //charging = gorillaScript.charging;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            if (nodeColor.text != currColor)
            {
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
            }
        } else if(coll.gameObject.tag == "Gorilla" && coll.GetComponent<GorillaMovement>().charging) // check if gorilla is charging when it collided
        {
            spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            if (nodeColor.text != currColor)
            {
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
            }
        }
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
