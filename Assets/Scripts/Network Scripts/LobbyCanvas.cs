using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCanvas : MonoBehaviour
{
    private GameObject networkManager = null;
    public Button startGameButton = null;

    [SerializeField] private GameObject lobbymenu;
    [SerializeField] private GameObject helpmenu;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager");
        if (!networkManager) Debug.Log("Could not find \"NetworkManager\"");
    }

    // Update is called once per frame
    private void Update()
    {
        startGameButton.interactable = (networkManager.GetComponent<NetworkManagerApeShip>()._allPlayersReady);
    }

    public void toggleHelp()
    {
        helpmenu.SetActive(!helpmenu.activeSelf);
    }
}
