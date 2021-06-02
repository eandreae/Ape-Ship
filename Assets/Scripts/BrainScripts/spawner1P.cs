using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class spawner1P : MonoBehaviour
{
    public NetworkManager nm;
    public Transform spawnPos;
    public GameObject neuronBlue;
    public GameObject neuronRed;
    public GameObject neuronGreen;
    public Text brainColor;
    Vector3 spawnLoc;

    public GameObject brainTarg;

    private bool fixRed;

    // Start is called before the first frame update
    void Start()
    {
        nm = GameObject.FindObjectOfType<NetworkManager>();
        if (nm)
            Object.Destroy(this.gameObject);
        
        fixRed = false;
    }

    public void Break() {
        fixRed = false;
    }

    public void Deposit() {
        if (brainTarg.GetComponent<NodeInstanceManager>().color == Color.red && fixRed == false)
            fixRed = true;
        else
            brainTarg.GetComponent<NodeInstanceManager>().FixNode();
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
    }
    public void Spawn2() {
        Spawn();
        Spawn();
    }
}
