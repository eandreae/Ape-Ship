using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CurrentRoom : MonoBehaviour
{
    public string roomName;
    NetworkManager nm;
    Text currentRoomText;

    void Start()
    {
        currentRoomText = GameObject.Find("CurrentRoomText").GetComponent<Text>();
        nm = GameObject.FindObjectOfType<NetworkManager>();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            // if there IS a network manager, only change text if it's the Local Player.
            if ( !nm || other.GetComponent<Player>().isLocalPlayer )
                currentRoomText.text = roomName;
        }
    }
}
