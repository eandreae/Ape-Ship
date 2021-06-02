using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class nukeSpawner : NetworkBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee; // spawnee will be networkNuke prefab
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
        vendingMachine = GameObject.Find("NukeVendingMachine");
    }

    // Update is called once per frame
    void Update()
    {
        //charging = gorillaScript.charging;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && isServer && canSpawn) // player can spawn items from vending machine on a cooldown
        {
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            SpawnObject(spawnLoc, spawnPos.rotation);
            canSpawn = false;
            StartCoroutine("SpawnTimer", 10.0f); // add 5 second cd to using vending machine
        }
        else if (coll.gameObject.tag == "Gorilla" && coll.GetComponent<GorillaMovement>().charging && isServer) // gorilla collision when charging means spawn item no matter what
        {
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 1.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 1.0f));
            SpawnObject(spawnLoc, spawnPos.rotation);
            canSpawn = false;
            StartCoroutine("SpawnTimer", 2.0f); // add 2 second cd to using vending machine
        }
    }

    IEnumerator SpawnTimer(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }

    public void SpawnObject(Vector3 spawnLoc, Quaternion rotation){
        GameObject temp = Instantiate(spawnee, spawnLoc, rotation);
        
        temp.GetComponent<Rigidbody>().useGravity = true;
        temp.GetComponent<destroyer>().enabled = true;

        NetworkServer.Spawn(temp);
    }

}
