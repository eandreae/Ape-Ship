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
    public GameObject bat;
    public GameObject batSpawn;

    public GameObject waypointArrow;

    UIManager uim;
    private float startval = -1.7f;
    private float endval = -5f;
    public bool batteryReplaced = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        uim = FindObjectOfType<UIManager>();

        progBar = GameObject.Find("ProgressSlider");
        bar = progBar.GetComponent<ProgressBar>();
        GameObject.Find("BatteryCopy").SetActive(true);

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

        if(batteryReplaced)
        {
            GameObject.Find("BatteryCopy").SetActive(true);
        } else
        {
            GameObject.Find("BatteryCopy").SetActive(false);
        }
    }

    private void SpawnBattery()
    {
        GameObject battery = GameObject.Find("BatteryWithAnimations");
        battery.tag = "Pick Up";
        battery.layer = 15;

        GameObject glass = GameObject.Find("Glass");
        //while(startval > -5)
        //{
        startval = endval;
            //startval = Mathf.Lerp(startval, endval, Time.deltaTime);
            glass.gameObject.transform.localPosition = new Vector3(glass.gameObject.transform.localPosition.x, startval, glass.gameObject.transform.localPosition.z);
        //}
        //battery = Instantiate(bat, batSpawn.transform.position, batSpawn.transform.rotation);
        //battery.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(batteryReplaced && other.gameObject.tag == "Player" && this.gameObject.name == "Escape2")
        {
            teleport = true;
            StartCoroutine("TeleportWait");
        }
        if (canEscape == true)
        {
            if (other.gameObject.name == "BatteryWithAnimations" && this.gameObject.name == "Escape")
            {
                Destroy(GameObject.Find("BatteryWithAnimations"));
                batteryReplaced = true;
                GameObject.Find("Escape2").GetComponent<Escape>().batteryReplaced = true;
            }
        }
    }

    IEnumerator TeleportWait()
    {
        waypointArrow.SetActive(false);
        yield return new WaitForSeconds(2f);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        gm.Victory();
    }

}
