using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class lungSpawner1P : MonoBehaviour
{
    public NetworkManager nm;
    public Transform spawnPos;
    public Transform spawnPos2;
    public Transform spawnPos3;
    public GameObject spawnee;
    string currColor;
    public Text nodeColor;
    Vector3 spawnLoc;
    GameObject canister1;
    GameObject canister2;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {

        nm = GameObject.FindObjectOfType<NetworkManager>();
        
        if (nm) {
            Object.Destroy(this.gameObject);
        }

        currColor = nodeColor.text;
        if(gameObject.name == "(1P) AirPump(O1)")
        {
            canister1 = GameObject.Find("1p Air_Tank_1(O1)");
            canister2 = GameObject.Find("1p Air_Tank_2(O1)");
            if (nm){
                spawnee = (nm.spawnPrefabs[9]);
            }
        } 
        else if(gameObject.name == "(1P) AirPump")
        {
            canister1 = GameObject.Find("1p Air_Tank_1");
            canister2 = GameObject.Find("1p Air_Tank_2");
            if (nm){
                spawnee = (nm.spawnPrefabs[10]);
            }
        }
        canister1.SetActive(true);
        canister2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fix()
    {
        if (nodeColor.text == "red")
        {
            canister1.SetActive(false);
            canister2.SetActive(false);
        }
        else if (nodeColor.text == "yellow")
        {
            canister1.SetActive(true);
            canister2.SetActive(false);
        }
        else if (nodeColor.text == "green")
        {
            canister1.SetActive(true);
            canister2.SetActive(true);
        }

        if (nodeColor.text == "green")
        {
            //currColor = nodeColor.text;
            if(gameObject.name == "AirPump(O1)")
            {
                if (GameObject.Find("Air_Tank_3(O1)(Clone)") != null)
                {
                    target = GameObject.Find("Air_Tank_3(O1)(Clone)");
                    Destroy(target);
                }

            } else if(gameObject.name == "AirPump")
            {
                if (GameObject.Find("Air_Tank_3(Clone)") != null)
                {
                    target = GameObject.Find("Air_Tank_3(Clone)");
                    Destroy(target);
                }

            }
        }

    }

    public void Spawn()
    {
        int choose = Random.Range(0, 3);
        if(choose == 0)
        {
            spawnLoc = new Vector3((float)spawnPos.position.x, (float)spawnPos.position.y, (float)spawnPos.position.z);
        } else if(choose == 1)
        {
            spawnLoc = new Vector3((float)spawnPos2.position.x, (float)spawnPos2.position.y, (float)spawnPos2.position.z);
        } else
        {
            spawnLoc = new Vector3((float)spawnPos3.position.x, (float)spawnPos3.position.y, (float)spawnPos3.position.z);
        }

        if (nodeColor.text != currColor)
        {
            if (nodeColor.text != "green")
            {
                //currColor = nodeColor.text;
                GameObject temp;
                if (nm){
                    temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                    temp.GetComponent<Rigidbody>().useGravity = true;
                    NetworkServer.Spawn(temp);
                }
                else {
                    temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                    temp.GetComponent<Rigidbody>().useGravity = true;
                    //temp.GetComponent<destroyer>().enabled = true;
                }
            }
        }

    }
}
