using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    private bool canEscape = false;
    private bool shouldSpawn = true;
    public bool teleport = false;
    GameManager gm;
    ProgressBar bar;
    GameObject progBar;
    GameObject battery;

    public GameObject waypointArrow;

    UIManager uim;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        uim = FindObjectOfType<UIManager>();

        progBar = GameObject.Find("ProgressSlider");
        bar = progBar.GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bar.teleport == true && shouldSpawn == true)
        {
            uim.ReplaceProgressBar();
            SpawnBattery();
            waypointArrow.SetActive(true);
            waypointArrow.GetComponent<Waypoint>().WhichWaypoint(7);
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
                teleport = true;
                Destroy(GameObject.Find("Battery(Clone)"));
                SpawnPlayer();
                StartCoroutine("TeleportWait");
            }
        }
    }

    private void SpawnPlayer()
    {
        //GameObject telePlayer = gameObject.Find("TeleportPlayerObject")
    }

    IEnumerator TeleportWait()
    {
        waypointArrow.SetActive(false);
        yield return new WaitForSeconds(5f); // charge for 1 second
        gm.Victory();
    }

}
