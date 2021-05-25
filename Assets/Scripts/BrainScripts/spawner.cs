using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject neuronBlue;
    public GameObject neuronRed;
    public GameObject neuronGreen;
    public Text brainColor;
    Vector3 spawnLoc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn() {
        spawnLoc = new Vector3(spawnPos.position.x + Random.Range(-4.0f,4.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(-3.0f, 3.0f));
        GameObject spawnee = neuronBlue;

        int sw = Random.Range(0, 3);
        switch(sw)
        {
            case 1:
                spawnee = neuronRed;
                break;
            case 2:
                spawnee = neuronGreen;
                break;
        }

        GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
        temp.GetComponent<Rigidbody>().useGravity = true;

        if(brainColor.text == "red")
        {
            spawnLoc = new Vector3(spawnPos.position.x + Random.Range(-4.0f, 4.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(-3.0f, 3.0f));
            GameObject spawnee2 = neuronBlue;

            int sw2 = Random.Range(0, 3);
            switch (sw2)
            {
                case 1:
                    spawnee = neuronRed;
                    break;
                case 2:
                    spawnee = neuronGreen;
                    break;
            }

            GameObject temp2 = Instantiate(spawnee2, spawnLoc, spawnPos.rotation);
            temp2.GetComponent<Rigidbody>().useGravity = true;

        }
    }
}
