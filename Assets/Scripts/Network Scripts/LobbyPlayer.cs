using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LobbyPlayer : NetworkBehaviour
{

    [SerializeField] private NetworkRoomPlayer player = null;
    bool currentvalue = false;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<NetworkRoomPlayer>();
        Debug.Log(player);
    }

    public void toggle()
    {
        player = this.GetComponent<NetworkRoomPlayer>();
        Debug.Log(player);
        player.CmdChangeReadyState(!currentvalue);
    }

    public void setbool(bool value)
    {
        player = this.GetComponent<NetworkRoomPlayer>();
        Debug.Log(player);
        player.CmdChangeReadyState(value);
        currentvalue = value;
    }
}
