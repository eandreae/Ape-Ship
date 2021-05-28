using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    private bool canEscape = false;
    private bool shouldSpawn = true;
    GameManager gm;
    ProgressBar bar;
    GameObject progBar;
    GameObject battery;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        progBar = GameObject.Find("ProgressSlider");
        bar = progBar.GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bar.teleport == true && shouldSpawn == true)
        {
            SpawnBattery();
            shouldSpawn = false;
            canEscape = true;
        }
    }

    private void SpawnBattery()
    {
        GameObject bat = GameObject.Find("Battery");
        bat.GetComponent<Rigidbody>().useGravity = true;
        GameObject batSpawn = GameObject.Find("BatterySpawn");
        battery = Instantiate(bat, batSpawn.transform.position, batSpawn.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canEscape == true)
        {
            if (other.gameObject.name == "Battery(Clone)")
            {
                gm.Victory();
            }
        }
    }

}
