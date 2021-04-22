using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerApeShip : NetworkManager
{
    public static int playerCount = 0;
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        playerCount++;
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
        GameObject cam = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "PlayerCamera"));
        NetworkServer.AddPlayerForConnection(conn, player);
        cam.GetComponent<PlayerCamera>().target = player;
        NetworkServer.Spawn(cam);
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
