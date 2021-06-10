using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HeartReactorCameraZoom : MonoBehaviour
{

    PlayerCamera pc;
    NetworkManager nm;

    void Start()
    {
        pc = FindObjectOfType<PlayerCamera>();
        nm = FindObjectOfType<NetworkManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!nm || other.GetComponent<Player>().isLocalPlayer)
                pc.ZoomOut();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            if(!nm || other.GetComponent<Player>().isLocalPlayer)
                pc.ZoomIn();
        }
    }
}
