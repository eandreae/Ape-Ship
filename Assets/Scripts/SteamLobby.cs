using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using Mirror;

public class SteamLobby : MonoBehaviour
{
    [SerializeField] private GameObject buttons = null;
    [SerializeField] private Text displayid = null;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private const string HostAddressKey = "HostAddress";

    private NetworkManager networkManager;


    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        if (!SteamManager.Initialized) return; //if steam isn't open then just stop
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    // Start is called before the first frame update
    public void HostLobby(int lobbysize)
    {
        //buttons.SetActive(false);

        if (lobbysize == 0)
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
        else
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, lobbysize);
    }

    public void StartSinglePlayer()
    {
        //buttons.SetActive(false);

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeInvisible, 1);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            //buttons.SetActive(true);
            return;
        }

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            HostAddressKey,
            SteamUser.GetSteamID().ToString());

        displayid.text = callback.m_ulSteamIDLobby.ToString();
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) return;

        string hostAddress = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            HostAddressKey);

        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        //buttons.SetActive(false);
    }

 //  private void InviteFriend()
 //  {
 //      SteamFriends.InviteUserToGame()
 //  }
}
