using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentRoom : MonoBehaviour
{
    public string roomName;

    Text currentRoomText;
    void Start()
    {
        currentRoomText = GameObject.Find("CurrentRoomText").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentRoomText.text = roomName;
        }
    }
}
