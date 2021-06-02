using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class spawner : NetworkBehaviour
{
    public NetworkManager nm;
    public Transform spawnPos;
    public GameObject neuronBlue;
    public GameObject neuronRed;
    public GameObject neuronGreen;
    public Text brainColor;
    Vector3 spawnLoc;

    // Start is called before the first frame update
    void Start()
    {
        nm = GameObject.FindObjectOfType<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn() {
        spawnLoc = new Vector3(spawnPos.position.x + Random.Range(-4.0f,4.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(-3.0f, 3.0f));
        
        //GameObject spawnee = nm.spawnPrefabs[6];
        GameObject spawnee = neuronBlue;

        int sw = Random.Range(0, 3);
        switch(sw)
        {
            case 1:
                //spawnee = nm.spawnPrefabs[7];
                spawnee = neuronRed;
                break;
            case 2:
                //spawnee = nm.spawnPrefabs[8];
                spawnee = neuronGreen;
                break;
        }

        if (isServer){
            GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
            temp.GetComponent<Rigidbody>().useGravity = true;
            NetworkServer.Spawn(temp);
        }

        if(brainColor.text == "red")
        {
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(-4.0f, 4.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(-3.0f, 3.0f));
            //GameObject spawnee2 = nm.spawnPrefabs[6];
            GameObject spawnee2 = neuronBlue;

            int sw2 = Random.Range(0, 3);
            switch (sw2)
            {
                case 1:
                    //spawnee2 = nm.spawnPrefabs[7];
                    spawnee2 = neuronRed;
                    break;
                case 2:
                    //spawnee2 = nm.spawnPrefabs[8];
                    spawnee2 = neuronGreen;
                    break;
            }

            if (nm && isServer){
                GameObject temp2 = Instantiate(spawnee2, spawnLoc, spawnPos.rotation);
                temp2.GetComponent<Rigidbody>().useGravity = true;
                NetworkServer.Spawn(temp2);
            }
        }
    }
}
