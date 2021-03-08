using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] playerCameras;
    public int maxCameras = 3;

    int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o") && maxCameras >= index)
        {
            playerCameras[index].SetActive(false);
            playerCameras[index + 1].SetActive(true);
            index++;
        }
        if (Input.GetKeyDown("i") && maxCameras >= index)
        {
            playerCameras[index].SetActive(false);
            playerCameras[index - 1].SetActive(true);
            index--;
        }
    }
}
