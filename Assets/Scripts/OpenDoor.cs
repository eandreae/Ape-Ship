using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    int count = 0;
    bool doorOpened = false;

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
            //Debug.Log(count);
            if (count < 100)
            {
                count += 1;
            }
            else if (count >= 100)
            {
                doorOpened = false;
                count = 0;
                transform.Translate(Vector3.forward * -7);
            }
        }
    }
}
