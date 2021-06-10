using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;


public class NetworkManagerApeShip : NetworkRoomManager
{

    private NetworkRoomManager networkManager;

    [Header("Start game button")]
    [SerializeField] private Button startbutton = null;
    
     // Array containing positions to spawn players in-game
    public Vector3[] spawnPos;

    List<NetworkConnection> previousconnections;

    public void Start() {
        spawnPos = new Vector3[] {
            new Vector3(36.0f, 0f,  3f), // p1
            new Vector3(41.5f, 0f,  3f), // p2
            new Vector3(36.0f, 0f, -3f), // p3
            new Vector3(41.5f, 0f, -3f), // p4
        };

        previousconnections = new List<NetworkConnection>();
    }

    public void StartGame()
    {
        ServerChangeScene("game");
    }

    public void ReturnToRoom()
    {
        ServerChangeScene("room");
    }
    
    public void Disconnect()
    {
        //under certain conditions, disconnecting will spawn a player in the main menu

        GameObject[] roomplayers = GameObject.FindGameObjectsWithTag("RoomPlayer");
        foreach (GameObject player in roomplayers)
        {
            NetworkRoomPlayer roomplayer = player.GetComponent<NetworkRoomPlayer>();
            if (roomplayer.isLocalPlayer)
                roomplayer.CmdChangeReadyState(true);
        }

        StopHost();
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
        GameObject player;

        if (numPlayers == 0)
        {
            player = Instantiate(roomPlayerPrefab.gameObject,                                     // gameobject
                                (playerPos + offset),                                             // new position
                                 roomPlayerPrefab.gameObject.GetComponent<Transform>().rotation); // rotation
        }
        else
        {
            player = Instantiate(spawnPrefabs[12].gameObject,                                     // gameobject
                                (playerPos + offset),                                             // new position
                                spawnPrefabs[12].gameObject.GetComponent<Transform>().rotation); // rotation
        }

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        //previousconnection = conn;
        //GameObject player = Instantiate(roomPlayerPrefab.gameObject, roomPlayerPrefab.gameObject.GetComponent<Transform>());
        //player.GetComponent<Player>().playerNum = numPlayers;
        //NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerSceneChanged(string newSceneName)
    {
        if (newSceneName == "game")
        {
            //replacing roomplayers with players when transitioning from room to game scene

            for (int i=roomSlots.Count-1; i>=0; i--)
            {
                // spawning PLAYER CLONES into game
                GameObject player;

                if (i == 0)
                {
                    player = Instantiate(playerPrefab,             // prefab/gameobject
                                         spawnPos[i],              // position
                                         Quaternion.identity);     // rotation
                }
                else
                {
                    player = Instantiate(spawnPrefabs[11].gameObject,    // gameobject
                                         spawnPos[i],                    // new position
                                         Quaternion.identity);           // rotation
                }


                //Debug.Log("spawn player at " + spawnPos[i]);

                //roomSlots[i].GetComponent<LobbyPlayer>().saveconnection(roomSlots[i].GetComponent<NetworkIdentity>().connectionToClient);
                previousconnections.Add(roomSlots[i].gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                
                NetworkServer.ReplacePlayerForConnection(roomSlots[i].GetComponent<NetworkIdentity>().connectionToClient, player, true);
            }
            // GameObject monkey = Instantiate( spawnPrefabs[1]);
            // GameObject gorilla = Instantiate( spawnPrefabs[2] );

            // NetworkServer.Spawn(monkey);
            // NetworkServer.Spawn(gorilla);
            base.OnServerSceneChanged(newSceneName);
        }
        if (newSceneName == "room")
        {
            //spawning players when returning to room from in game

            Debug.Log("returning to room");
            for (int i = 0; i < previousconnections.Count; i++)
            {
                Vector3 playerPos = roomPlayerPrefab.gameObject.GetComponent<Transform>().position;
                Vector3 offset = new Vector3(5f * (i), 0, -3);

                // spawn ROOM PLAYER at given transform with correct rotation
                GameObject player;
                if (i == previousconnections.Count-1)
                {
                    player = Instantiate(roomPlayerPrefab.gameObject,                                     // gameobject
                                        (playerPos + offset),                                             // new position
                                         roomPlayerPrefab.gameObject.GetComponent<Transform>().rotation); // rotation
                }
                else
                {
                    player = Instantiate(spawnPrefabs[12].gameObject,                                     // gameobject
                                        (playerPos + offset),                                             // new position
                                        spawnPrefabs[12].gameObject.GetComponent<Transform>().rotation); // rotation
                }

                NetworkServer.AddPlayerForConnection(previousconnections[i], player);
            }

            for (int i = 0; i < previousconnections.Count; i++) NetworkServer.SetClientReady(previousconnections[i]);
        }
    }

    public override void OnRoomClientSceneChanged(NetworkConnection conn)
    {
        base.OnRoomClientSceneChanged(conn);
    }


    public override void OnRoomServerPlayersReady()
    {
        //base.OnRoomServerPlayersReady();
        //base function automatically starts game when all are ready
    }

    public override void OnRoomServerPlayersNotReady()
    {
        base.OnRoomServerPlayersNotReady();
    }
}
