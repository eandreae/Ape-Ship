using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungMinigame : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    string currColor;
    public Text nodeColor;
    Vector3 spawnLoc;

    // Start is called before the first frame update
    void Start()
    {
        currColor = nodeColor.text;
    }

    // Update is called once per frame
    void Update()
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
