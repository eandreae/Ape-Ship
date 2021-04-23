using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerApeShip : NetworkManager
{
    public static int playerCount = 0;
    private NetworkManager networkManager;

    public override void OnStartServer()
    {
        networkManager = GetComponent<NetworkManager>();
        base.OnStartServer();
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        networkManager = GetComponent<NetworkManager>();
        base.OnServerConnect(conn);
        playerCount++;
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Log(numPlayers);

        GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
        player.GetComponent<Player>().playerNum = numPlayers;
        GameObject cam = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "TestCamera"));
        cam.GetComponent<PlayerCamera>().playerNum = numPlayers;
        NetworkServer.AddPlayerForConnection(conn, player);
        NetworkServer.Spawn(cam);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        playerCount--;
    }

    public void SetMaxConnections(int count)
    {
        networkManager.maxConnections = count;
    }
}
