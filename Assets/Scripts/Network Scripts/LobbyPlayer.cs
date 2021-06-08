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
        while (true)
        {
            player = this.GetComponent<NetworkRoomPlayer>();
            if (player.isLocalPlayer) break;
        }
        

        readybutton = GameObject.Find("ready").GetComponent<Button>();
        readybutton.onClick.AddListener(toggle);

        disconnect = GameObject.Find("disconnect").GetComponent<Button>();
        disconnect.onClick.AddListener(() => setbool(true));
        Debug.Log(player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            toggle();
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
        readyindicator.SetActive(!readyindicator.activeSelf);

        player.CmdChangeReadyState(!player.readyToBegin);
    }

    public void setbool(bool value)
    {
        player.CmdChangeReadyState(value);
    }
}
