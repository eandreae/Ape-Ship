using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class LobbyPlayer : NetworkBehaviour
{

    [SerializeField] private NetworkRoomPlayer player = null;

    [SerializeField] private Button readybutton = null;
    [SerializeField] private Button disconnect = null;

    [SerializeField] private GameObject readyindicator = null;



    public NetworkConnection previousConnection;

    bool currentvalue = false;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.GetComponent<NetworkRoomPlayer>();
        readybutton = GameObject.Find("ready").GetComponent<Button>();
        readybutton.onClick.AddListener(toggle);

        disconnect = GameObject.Find("disconnect").GetComponent<Button>();
        disconnect.onClick.AddListener(() => setbool(true));

        Debug.Log(player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player = this.gameObject.GetComponent<NetworkRoomPlayer>();
            Debug.Log(player);
            if (isLocalPlayer) toggle();
        }
           

        readyindicator.SetActive(player.readyToBegin);
    }


    public void saveconnection(NetworkConnection conn)
    {
        previousConnection = conn;
    }

    public NetworkConnection getconnection()
    {
        return previousConnection;
    }


    public void toggle()
    {
        player.CmdChangeReadyState(!player.readyToBegin);
    }

    public void setbool(bool value)
    {
        player.CmdChangeReadyState(value);
    }
}
