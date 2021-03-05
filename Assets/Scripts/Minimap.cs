using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject minimap;

    public bool canActivateMinimap = true;

    //Used in PauseMenu
    bool minimapOpen = false;

    //If true, player will have to hold down button. If not, then player can toggle on and off.
    public bool holdDown = true;

    // Update is called once per frame
    void Update()
    {
        if (holdDown)
        {
            //If the player holds down either M, Q, or Tab, the minimap will pop up so long as they are holding either of those keys down. 
            //Q and Tab are quick to hit, while M is instictive for some players.
            if (Input.GetKey("m") || Input.GetKey("q") || Input.GetKey(KeyCode.Tab))
            {
                //So you can't activate this while paused
                if (canActivateMinimap)
                {
                    minimap.SetActive(true);
                }
            }
            else
            {
                minimap.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKeyDown("m") || Input.GetKeyDown("q") || Input.GetKeyDown(KeyCode.Tab))
            {
                if (minimapOpen)
                {
                    minimap.SetActive(false);
                    minimapOpen = false;
                }
                else
                {
                    minimap.SetActive(true);
                    minimapOpen = true;
                }
            }
        }
    }
}
