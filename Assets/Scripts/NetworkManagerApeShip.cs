using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerApeShip : NetworkManager
{

    private NetworkManager networkManager;

    public GameObject poptionscanvas = null;
    public GameObject mplobbycanvas = null;

    private int maxconnections;
    private NetworkConnection[] connections;
    private int connectionCount = 0;
    

    public override void OnStartServer()
    {
        networkManager = GetComponent<NetworkManager>();
        maxconnections = networkManager.maxConnections;
        connections = new NetworkConnection[maxconnections];

        
        base.OnStartServer();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        poptionscanvas.SetActive(false);
        mplobbycanvas.SetActive(true);

        networkManager = GetComponent<NetworkManager>();
        base.OnServerConnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        poptionscanvas.SetActive(false);
        mplobbycanvas.SetActive(true);

        connections[connectionCount] = conn;
        connectionCount++;
    }


    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        poptionscanvas.SetActive(true);
        mplobbycanvas.SetActive(false);

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        poptionscanvas.SetActive(true);
        mplobbycanvas.SetActive(false);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        connectpls();
    }

    public void connectpls()
    {
        //this would be very messed up for players disconnecting while in lobby

        //weird stuff is happening

        int itr = 0;
        while (connections[itr] != null && itr < networkManager.maxConnections)
        {
            //Debug.Log("numplayers init:" + numPlayers);
            GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
            player.GetComponent<Player>().playerNum = numPlayers + 1;
            //Debug.Log("before adding connect:" + numPlayers);
            NetworkServer.AddPlayerForConnection(connections[itr], player);
            //Debug.Log("after spawn:" + numPlayers);
            //GameObject cam = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "TestCamera"));
            //cam.GetComponent<PlayerCamera>().playerNum = numPlayers;

            //NetworkServer.Spawn(cam);
            itr++;
        }

    }

    
}
