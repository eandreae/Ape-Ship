using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StartGame : NetworkBehaviour
{
    private GameObject networkManager = null;
    public GameObject startGameButton = null;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager");
        if (!networkManager) Debug.Log("Could not find \"NetworkManager\"");


    }

    // Update is called once per frame
    private void Update()
    {
        startGameButton.SetActive(networkManager.GetComponent<NetworkManagerApeShip>()._allPlayersReady);
    }



    [Command]
    public void CmdStartGame()
    {
        networkManager.GetComponent<NetworkManagerApeShip>().ServerChangeScene("game");

    }
}
