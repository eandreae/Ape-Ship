﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {   
        nm = GameObject.FindObjectOfType<NetworkManager>();
        if (nm){
            Object.Destroy(this.gameObject);
        }

        currColor = nodeColor.text;
        canSpawn = true;
        // foodItems = new GameObject[9]; // 33% sandwich, 33% kebab, 22% banana, 11% nuke
        // foodItems[0] = GameObject.Find("KrillSandwich");
        // foodItems[1] = GameObject.Find("KrillSandwich");
        // foodItems[2] = GameObject.Find("KrillSandwich");
        // foodItems[3] = GameObject.Find("SeaFoodKebab");
        // foodItems[4] = GameObject.Find("SeaFoodKebab");
        // foodItems[5] = GameObject.Find("SeaFoodKebab");
        // foodItems[6] = GameObject.Find("Banana");
        // foodItems[7] = GameObject.Find("Banana");
        // foodItems[8] = GameObject.Find("SodaNuke");
        

        spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
        //gorillaScript = GetComponent<GorillaMovement>();

        vendingMachine = GameObject.Find("TexturedVendingMachine");
        foodAnim = vendingMachine.GetComponent<Animator>();

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
            //spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            //Changed to always spawn
            //if (nodeColor.text != "green")
            //{
                foodAnim.Play("PushButton");
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
                temp.GetComponent<destroyer>().enabled = true;
                canSpawn = false;
                StartCoroutine("SpawnTimer", 5.0f); // add 5 second cd to using vending machine
            //}
        } else if(coll.gameObject.tag == "Gorilla" && coll.GetComponent<Gorilla1P>().charging) // gorilla collision when charging means spawn item no matter what
        {
            //spawnee = foodItems[ Random.Range(0, foodItems.Length) ]; // get a random foodItem to spawn
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            //Changed to always spawn
            //if (nodeColor.text != "green")
            //{
                foodAnim.Play("PushButton");
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
                temp.GetComponent<destroyer>().enabled = true;
                canSpawn = false;
                StartCoroutine("SpawnTimer", 2.0f); // add 2 second cd to using vending machine
            //}
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
