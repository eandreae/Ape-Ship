using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerApeShip : NetworkManager
{
    public static int playerCount = 0;
    
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
        NetworkServer.AddPlayerForConnection(conn, player);
        playerCount++;
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        playerCount--;
    }

    public void Start()
    {
        base.Start();
    }

    public void Update()
    {
        base.LateUpdate();
    }
}
