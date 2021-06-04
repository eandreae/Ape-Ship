using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        /*
        NetworkServer.DisconnectAllExternalConnections();
        int count = previousconnections.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomplayer = Instantiate(roomPlayerPrefab.gameObject, // prefab/gameobject
                                                spawnPos[i],                 // position
                                                Quaternion.identity);        // rotation
            NetworkServer.AddPlayerForConnection(previousconnections[i], roomplayer);
            NetworkServer.SetClientReady(previousconnections[i]);
        }
        previousconnections.Clear();
        */
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

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        NetworkServer.SetClientReady(conn);
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
                //roomSlots[i].GetComponent<LobbyPlayer>().saveconnection(roomSlots[i].GetComponent<NetworkIdentity>().connectionToClient);
                previousconnections.Add(roomSlots[i].gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                NetworkServer.ReplacePlayerForConnection(roomSlots[i].GetComponent<NetworkIdentity>().connectionToClient, player, true);
                
                //NetworkServer.Destroy(roomplayer);
            }
            // GameObject monkey = Instantiate( spawnPrefabs[1]);
            // GameObject gorilla = Instantiate( spawnPrefabs[2] );
            
            // NetworkServer.Spawn(monkey);
            // NetworkServer.Spawn(gorilla);
        }
        if (newSceneName == "room")
        {
            Debug.Log("returning to room");
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
        Debug.Log("ready");
        //startbutton.interactable = true;
    }

    public override void OnRoomServerPlayersNotReady()
    {
        base.OnRoomServerPlayersNotReady();
        Debug.Log("not ready");
        //startbutton.interactable = false;
    }
}
