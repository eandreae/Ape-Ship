using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject minimap;

    // Update is called once per frame
    void Update()
    {
        //If the player holds down either Q or M, the minimap will pop up so long as they are holding either of those keys down. 
        //Q is quick to hit, while M is instictive for some players.
        if (Input.GetKey("m") || Input.GetKey("q"))
        {
            minimap.SetActive(true);
        }
        else
        {
            minimap.SetActive(false);
        }
    }
}
