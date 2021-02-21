using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    //int count = 0;
    bool doorOpened = false;
    private float currTime = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")||other.CompareTag("Monkey") || other.CompareTag("Gorilla")) && doorOpened == false)
        {
            transform.Translate(Vector3.forward * 7);
            FindObjectOfType<AudioManager>().Play("OpenDoor");
            doorOpened = true;
        }
    }

    void Update()
    {
        if (doorOpened == true)
        {
            currTime += Time.deltaTime;
            //Debug.Log(count);
            if (currTime < 1.0)
            {
                //do nothing
            }
            else if (currTime >= 1.0)
            {
                doorOpened = false;
                currTime = 0.0f;
                transform.Translate(Vector3.forward * -7);
            }
        }
    }
}
