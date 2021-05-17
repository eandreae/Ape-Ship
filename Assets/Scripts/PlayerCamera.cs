// Controls the movement of the Camera that follows the Player.
// Referenced https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    //public NetworkManager nm;
    public GameObject target;
    public Vector3 offset = new Vector3(0, 19f, -10);
    public int playerNum = -1;
    Text currentRoomText;
    private float fadeNum = 0.01f;

    void Start() {

        //nm = FindObjectOfType<NetworkManager>();
        target = GameObject.Find("_DEBUGGER_PLAYER");
        //        Debug.Log("nm.numPlayers:" + nm.numPlayers);
        //        this.playerNum = nm.numPlayers;
        // foreach (GameObject p in playerList){
        //     //Debug.Log("player's num: " + p.GetComponent<Player>().playerNum);
        //     //Debug.Log("cam's num: " + this.playerNum);
            
        //     if (p.GetComponent<Player>().playerNum == nm.playerNum){
        //         target = p;
        //         break;
        //     }
        // }

    }
    private void Update()
    {
        //Debug.Log(fadeNum * Time.deltaTime);
        currentRoomText = GameObject.Find("CurrentRoomText").GetComponent<Text>();
        if (currentRoomText.text == "Heart Reactor")
        {
            while (this.gameObject.GetComponent<Camera>().orthographicSize < 20f)
            {
                this.gameObject.GetComponent<Camera>().orthographicSize = this.gameObject.GetComponent<Camera>().orthographicSize + (fadeNum * Time.deltaTime);
            }
        }
        else
        {
            while (this.gameObject.GetComponent<Camera>().orthographicSize > 11f)
            {
                this.gameObject.GetComponent<Camera>().orthographicSize -= fadeNum * Time.deltaTime;
            }
        }
    }

    // Used with camera
    private Vector3 shakeOffset = new Vector3(0,0,0);

    // Update is called once per frame
    void LateUpdate()
    {
        if(!target){
            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
            //target = playerList[playerList.Length - 1];
            foreach (GameObject player in playerList){
                if (player.GetComponent<Player>().isLocalPlayer) {
                    target = player;
                    break;
                }
            }
        }
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition + shakeOffset;
    }
    public void OnBrokenStomach()
    {
        Debug.Log("Broken Stomach");
        StartCoroutine(Rumble(2, 0.5f, 15));
    }

    public void OnFixedStomach()
    {
        Debug.Log("Fixed Stomach");
        StopAllCoroutines();
    }

    IEnumerator Rumble (float duration, float magnitude, float frequency)
    {
        // Infinite loop is broken by StopAllCoroutines from StomachTarget On Node Fix
        Debug.Log("Rumble Start");
        while(true) {
            StartCoroutine(Shake(duration, magnitude));
            float elapsed = 0.0f;
            while (elapsed < frequency) {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator Shake (float duration, float magnitude)
    {
        Debug.Log("Shake Start");
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            shakeOffset.Set(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        shakeOffset.Set(0,0,0);
    }
}
