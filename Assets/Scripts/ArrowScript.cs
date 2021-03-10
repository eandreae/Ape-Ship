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
    GameObject temp;
    static bool started = false;
    static int rand;
    static int count;
    List<GameObject> arrowsList = new List<GameObject>();
    List<GameObject> tileList = new List<GameObject>();

    static readonly string[] arrowNames =  {
        "Arrow_Left",
        "Arrow_Right",
        "Arrow_Down",
        "Arrow_Up"
    };
    static readonly string[] tileNames =
    {
        "LEFT",
        "RIGHT",
        "DOWN",
        "UP"
    };
    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        foreach(string name in arrowNames)
        {
            arrowsList.Add(GameObject.Find(name));
            temp = GameObject.Find(name);
            temp.SetActive(true);
        }
        foreach(string name in tileNames)
        {
            tileList.Add(GameObject.Find(name));
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*if (started)
        {
            ChoosePattern();
        }*/
    }

    private void ChoosePattern()
    {
        //randomly choose arrow for player to press
        rand = Random.Range(0, 4);
        //Debug.Log(rand);
        arrowsList[rand].SetActive(true);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if(gameObject.name == "Center" && started == false)
            {
                foreach (GameObject obj in arrowsList)
                {
                    obj.SetActive(false);
                }

                started = true;
                ChoosePattern();
            }
            //turn on all arrows if correct non-center tile is touched
            if(started == true && tileList[rand].name == gameObject.name && gameObject.name != "Center")
            {
                arrowsList[rand].SetActive(false);
                if(count == 5){
                    foreach (GameObject obj in arrowsList)
                    {
                        obj.SetActive(true);
                    }
                } else
                {
                    ++count;
                    ChoosePattern();
                }
                //turn off all arrows if incorrect non-center tile is touched
            }
            else if (started == true && tileList[rand] != gameObject && gameObject.name != "Center")
            {
                foreach (GameObject obj in arrowsList)
                {
                    obj.SetActive(false);
                }

            }

            /*if (gameObject.name == "LEFT")
            {
                //arrowsList[0].SetActive(true);
            }
            else if (gameObject.name == "UP")
            {
                //arrowsList[3].SetActive(true);
            }
            else if (gameObject.name == "RIGHT")
            {
                //arrowsList[1].SetActive(true);
            }
            else if (gameObject.name == "DOWN")
            {
                //arrowsList[2].SetActive(true);
            }*/

        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (gameObject.name == "LEFT")
            {
                //arrowsList[0].SetActive(false);
            }
            else if (gameObject.name == "UP")
            {
                //arrowsList[3].SetActive(false);
            }
            else if (gameObject.name == "RIGHT")
            {
                //arrowsList[1].SetActive(false);
            }
            else if (gameObject.name == "DOWN")
            {
                //arrowsList[2].SetActive(false);
            }

        }

    }
}
