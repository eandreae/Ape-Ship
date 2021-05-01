using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerApeShip : NetworkRoomManager
{

    private NetworkRoomManager networkManager;

    [Header("")]

    private int maxconnections;
    
    public override void OnStartServer()
    {
        networkManager = GetComponent<NetworkRoomManager>();
        maxconnections = networkManager.maxConnections;

        base.OnStartServer();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        networkManager = GetComponent<NetworkRoomManager>();
        base.OnServerConnect(conn);
    }

    
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
      //  //Debug.Log("numplayers init:" + numPlayers);
      //  GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
      //  player.GetComponent<Player>().playerNum = numPlayers + 1;
      //  //Debug.Log("before adding connect:" + numPlayers);
      //  NetworkServer.AddPlayerForConnection(conn, player);
      //  //Debug.Log("after spawn:" + numPlayers);
      //  //GameObject cam = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "TestCamera"));
      //  //cam.GetComponent<PlayerCamera>().playerNum = numPlayers;
      //
      //  //NetworkServer.Spawn(cam);
    }




    public override void OnRoomServerAddPlayer(NetworkConnection conn)
    {
        //base.OnRoomServerAddPlayer(conn);

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
    }

    public override void OnRoomClientConnect(NetworkConnection conn)
    {
        base.OnRoomClientConnect(conn);
        
        Debug.Log("room client connect");

        
        //Debug.Log("numplayers init:" + numPlayers);
        GameObject player = Instantiate(roomPlayerPrefab.gameObject, roomPlayerPrefab.gameObject.GetComponent<Transform>());
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnRoomClientEnter()
    {
        base.OnRoomClientEnter();
        Debug.Log("room client enter");
    }


}
