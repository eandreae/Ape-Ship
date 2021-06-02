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
            pc.ZoomOut();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pc.ZoomIn();
        }
    }
}
