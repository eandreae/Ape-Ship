using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerApeShip : NetworkRoomManager
{

    private NetworkRoomManager networkManager;


    [Header("")]

    private int maxconnections;
    private List<NetworkConnection> connections { get; } = new List<NetworkConnection>();

    public override void OnStartServer()
    {
        networkManager = GetComponent<NetworkRoomManager>();
        maxconnections = networkManager.maxConnections;

        base.OnStartServer();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        networkManager = GetComponent<NetworkRoomManager>();

        connections.Add(conn);

        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        connections.Remove(conn);

        base.OnServerDisconnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
    }




    public override void OnRoomServerAddPlayer(NetworkConnection conn)
    {
        base.OnRoomServerAddPlayer(conn);
        Debug.Log("room server add player");
    }

    public override void OnRoomStartServer()
    {
        base.OnRoomStartServer();
        Debug.Log("room start server");
    }

    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);
        
        Debug.Log("room server connect (conn: " + conn +")");
    }

    public override void OnRoomServerSceneChanged(string sceneName)
    {
        base.OnRoomServerSceneChanged(sceneName);
        Debug.Log("room server scene changed");

        if (sceneName == "game")
        {
            for (int i=0; i<=numPlayers; i++)
            {
                //Debug.Log("numplayers init:" + numPlayers);
                GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
                player.GetComponent<Player>().playerNum = numPlayers + 1;
                //Debug.Log("before adding connect:" + numPlayers);
                NetworkServer.AddPlayerForConnection(connections[i], player);
                //Debug.Log("after spawn:" + numPlayers);
                //GameObject cam = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "TestCamera"));
                //cam.GetComponent<PlayerCamera>().playerNum = numPlayers;

                //NetworkServer.Spawn(cam);
            }
        }
    }

    public override void OnRoomClientConnect(NetworkConnection conn)
    {
        base.OnRoomClientConnect(conn);
        
        Debug.Log("room client connect");

        
        //Debug.Log("numplayers init:" + numPlayers);
        //GameObject player = Instantiate(roomPlayerPrefab.gameObject, roomPlayerPrefab.gameObject.GetComponent<Transform>());
        //NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnRoomClientDisconnect(NetworkConnection conn)
    {
        base.OnRoomClientDisconnect(conn);
        Debug.Log("room client disconnect");
    }

    public override void OnRoomClientEnter()
    {
        base.OnRoomClientEnter();
        Debug.Log("room client enter");
    }


}
