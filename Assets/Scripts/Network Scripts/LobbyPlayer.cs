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
    bool currentvalue = false;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<NetworkRoomPlayer>();

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

    public void toggle()
    {
        player.CmdChangeReadyState(!player.readyToBegin);
        currentvalue = !currentvalue;
    }

    public void setbool(bool value)
    {
        player.CmdChangeReadyState(value);
        currentvalue = value;
    }
}
