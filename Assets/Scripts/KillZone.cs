using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.tag == "Player"){ // in case player falls in
            Debug.Log("Player entered the Kill Zone.");
            gm.Defeat(4);
        }

        Object.Destroy(other);
    }
}
