using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    GameObject upArrow;
    GameObject downArrow;
    GameObject rightArrow;
    GameObject leftArrow;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        leftArrow = GameObject.Find("Arrow_Left");
        leftArrow.SetActive(true);
        rightArrow = GameObject.Find("Arrow_Right");
        rightArrow.SetActive(true);
        downArrow = GameObject.Find("Arrow_Down");
        downArrow.SetActive(true);
        upArrow = GameObject.Find("Arrow_Up");
        upArrow.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //upArrow.SetActive(true);
    }

    private void OnTriggerEnter(Collider coll)
    {
        leftArrow.SetActive(false);
        upArrow.SetActive(false);
        rightArrow.SetActive(false);
        downArrow.SetActive(false);

        if (coll.gameObject.name == "Player")
        {
            //Debug.Log(gameObject.name);
            if(gameObject.name == "LEFT")
            {
                //Debug.Log("YEEE");
                leftArrow.SetActive(true);
            }
            else if (gameObject.name == "UP")
            {
                upArrow.SetActive(true);
            }
            else if (gameObject.name == "RIGHT")
            {
                rightArrow.SetActive(true);
            }
            else if (gameObject.name == "DOWN")
            {
                downArrow.SetActive(true);
            }

        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (gameObject.name == "LEFT")
            {
                leftArrow.SetActive(false);
            }
            else if (gameObject.name == "UP")
            {
                upArrow.SetActive(false);
            }
            else if (gameObject.name == "RIGHT")
            {
                rightArrow.SetActive(false);
            }
            else if (gameObject.name == "DOWN")
            {
                downArrow.SetActive(false);
            }

        }

    }
}
