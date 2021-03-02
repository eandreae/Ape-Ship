using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    string currColor;
    public Text nodeColor;
    Vector3 spawnLoc;
    GameObject canister1;
    GameObject canister2;

    // Start is called before the first frame update
    void Start()
    {
        currColor = nodeColor.text;
        canister1 = GameObject.Find("Air_Tank_1");
        canister2 = GameObject.Find("Air_Tank_2");
        canister1.SetActive(true);
        canister2.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 5.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 5.0f));


        if (nodeColor.text == "red")
        {
            canister1.SetActive(false);
            canister2.SetActive(false);
        }
        else if (nodeColor.text == "yellow")
        {
            canister1.SetActive(true);
            canister2.SetActive(false);
        }/*
        else if (nodeColor.text == "green")
        {
            canister1.active = true;
            canister2.active = true;
        }*/

        if (nodeColor.text != currColor)
        {
            currColor = nodeColor.text;
            GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
            temp.GetComponent<Rigidbody>().useGravity = true;
        }

    }

    public void Spawn() {
        //spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f,8.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 5.0f));
        //GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
        //temp.GetComponent<Rigidbody>().useGravity = true;
    }
}
