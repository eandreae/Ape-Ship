// Controls the movement of the Camera that follows the Player.
// Referenced https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //public NetworkManager nm;
    public GameObject target;
    public Vector3 offset = new Vector3(0, 19f, -10);
    public int playerNum = -1;

    void Start() {

//        nm = FindObjectOfType<NetworkManager>();
//        Debug.Log("nm.numPlayers:" + nm.numPlayers);
//        this.playerNum = nm.numPlayers;

        // foreach (GameObject p in playerList){
        //     //Debug.Log("player's num: " + p.GetComponent<Player>().playerNum);
        //     //Debug.Log("cam's num: " + this.playerNum);
            
        //     if (p.GetComponent<Player>().playerNum == nm.playerNum){
        //         target = p;
        //         break;
        //     }
        // }


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!target){
            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
            target = playerList[playerList.Length - 1];
        }
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition;
    }
}
