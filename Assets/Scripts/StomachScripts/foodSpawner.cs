using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class foodSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    string currColor;
    public Text nodeColor;
    Vector3 spawnLoc;
    GorillaMovement gorillaScript;
    private bool charging;

    // Start is called before the first frame update
    void Start()
    {
        currColor = nodeColor.text;
        gorillaScript = GetComponent<GorillaMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        charging = gorillaScript.charging;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            if (nodeColor.text != currColor)
            {
                currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
            }
        } else if(coll.gameObject.tag == "Gorilla")
        {
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
