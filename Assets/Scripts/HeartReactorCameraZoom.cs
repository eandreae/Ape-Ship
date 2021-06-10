using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartReactorCameraZoom : MonoBehaviour
{

    PlayerCamera pc;

    void Start()
    {
        pc = FindObjectOfType<PlayerCamera>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Player>() && other.GetComponent<Player>().isLocalPlayer)
                pc.ZoomOut();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            if(other.GetComponent<Player>() && other.GetComponent<Player>().isLocalPlayer)
                pc.ZoomIn();
        }
    }
}
