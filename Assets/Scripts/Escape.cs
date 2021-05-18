using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    private bool canEscape = false;
    GameManager gm;
    ProgressBar bar;
    GameObject progBar;

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
        if(bar.teleport == true)
        {
            canEscape = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canEscape == true)
        {
            if (other.gameObject.tag == "Player")
            {
                gm.Victory();
            }
        }
    }

}
