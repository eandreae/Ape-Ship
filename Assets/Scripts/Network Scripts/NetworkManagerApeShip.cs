﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;




public class NetworkManagerApeShip : NetworkRoomManager
{

    private NetworkRoomManager networkManager;

    [Header("Start game button")]
    [SerializeField] private GameObject startbutton = null;
    
     // Array containing positions to spawn players in-game
    public Vector3[] spawnPos;

    List<NetworkRoomPlayer> previousRoomSlots;

    public void Start() {
        spawnPos = new Vector3[] {
            new Vector3(36.0f, 0f,  3f), // p1
            new Vector3(41.5f, 0f,  3f), // p2
            new Vector3(36.0f, 0f, -3f), // p3
            new Vector3(41.5f, 0f, -3f), // p4
        };

        previousRoomSlots = new List<NetworkRoomPlayer>();
    }

    public void StartGame()
    {
        ServerChangeScene("game");
    }

    public void ReturnToRoom()
    {
        int count = previousRoomSlots.Count;
        for (int i = 0; i < count; i++)
        {
            NetworkServer.ReplacePlayerForConnection(previousRoomSlots[i].GetComponent<LobbyPlayer>().getconnection(), previousRoomSlots[i].gameObject);
        }
        previousRoomSlots.Clear();

        ServerChangeScene("room");
    }


    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        /*
         * remote client cameras are locked on roomplayers
        */

        

        base.OnServerConnect(conn);

        //Debug.Log(conn);
        
        Vector3 playerPos = roomPlayerPrefab.gameObject.GetComponent<Transform>().position;
        Vector3 offset = new Vector3(5f * numPlayers, 0, -3);
        //GameObject player = Instantiate(roomPlayerPrefab.gameObject, roomPlayerPrefab.gameObject.GetComponent<Transform>());
        
        // spawn ROOM PLAYER at given transform with correct rotation
        GameObject player = Instantiate(   roomPlayerPrefab.gameObject,                                     // gameobject
                                          (playerPos + offset),                                             // new position
                                           roomPlayerPrefab.gameObject.GetComponent<Transform>().rotation); // rotation

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);

        //GameObject player = Instantiate(roomPlayerPrefab.gameObject, roomPlayerPrefab.gameObject.GetComponent<Transform>());
        //player.GetComponent<Player>().playerNum = numPlayers;
        //NetworkServer.AddPlayerForConnection(conn, player);
    }


    public override void OnServerSceneChanged(string newSceneName)
    {
        base.OnServerSceneChanged(newSceneName);

        if (newSceneName == "game"){
            
            for (int i=0; i<roomSlots.Count; i++)
            {
                // spawning PLAYER CLONES into game
                GameObject player = Instantiate(  playerPrefab,             // prefab/gameobject
                                                  spawnPos[i],              // position
                                                  Quaternion.identity);     // rotation
                //Debug.Log("spawn player at " + spawnPos[i]);

                //GameObject roomplayer = roomSlots[0].gameObject;
                roomSlots[i].GetComponent<LobbyPlayer>().saveconnection(roomSlots[i].GetComponent<NetworkIdentity>().connectionToClient);
                previousRoomSlots.Add(roomSlots[i]);
                NetworkServer.ReplacePlayerForConnection(roomSlots[i].GetComponent<NetworkIdentity>().connectionToClient, player);
                //NetworkServer.Destroy(roomplayer);
            }
            // GameObject monkey = Instantiate( spawnPrefabs[1]);
            // GameObject gorilla = Instantiate( spawnPrefabs[2] );
            
            // NetworkServer.Spawn(monkey);
            // NetworkServer.Spawn(gorilla);
        }
    }
    
    public override void OnClientConnect(NetworkConnection conn){
        base.OnClientConnect(conn);
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
    }



    public override void OnRoomServerPlayersReady()
    {
        //base.OnRoomServerPlayersReady();
        //base function automatically starts game when all are ready
        Debug.Log("ready");
        startbutton.SetActive(true);
    }

    public override void OnRoomServerPlayersNotReady()
    {
        base.OnRoomServerPlayersNotReady();
        Debug.Log("not ready");
        startbutton.SetActive(false);
    }

    public override void OnRoomClientConnect(NetworkConnection conn)
    {
        base.OnRoomClientConnect(conn);
        Debug.Log("room client connect");
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
