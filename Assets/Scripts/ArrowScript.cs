using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ArrowScript : MonoBehaviour
{
    private Animator animator;
    GameObject heartObj;
    GameObject temp;
    static bool started = false;
    static int rand;
    static int count;
    //static bool broken = false;
    List<GameObject> arrowsList = new List<GameObject>();
    List<GameObject> tileList = new List<GameObject>();
    public UnityEvent DanceComplete;
    static bool flashing;
    static bool paused;
    public Text HeartColor;
    Material mat;

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
        heartObj = GameObject.Find("Heart_Reactor_3");
        animator = heartObj.GetComponent<Animator>();
        animator.SetInteger("Color", 5);


        count = 0;
        //broken = false;
        foreach(string name in arrowNames)
        {
            arrowsList.Add(GameObject.Find(name));
            //temp = GameObject.Find(name);
            //temp.SetActive(true);
        }
        foreach(string name in tileNames)
        {
            tileList.Add(GameObject.Find(name));
        }

        if (gameObject.name != "Center")
        {
            foreach (GameObject obj in arrowsList)
            {
                mat = obj.GetComponent<Renderer>().material;
                mat.EnableKeyword("_EMISSION");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name != "Center")
        {
            //mat.DisableKeyword("_EMISSION");
        }
    }

    public void ChangeBroken()
    {
        if (HeartColor.text == "green")
        {
            //broken = false;
        } else
        {
            animator.SetInteger("Color", 0);
            foreach (GameObject obj in arrowsList)
            {
                mat = obj.GetComponent<Renderer>().material;
                mat.DisableKeyword("_EMISSION");
            }
            //broken = true;
            count = 0;
            started = true;
            ChoosePattern();
        }
    }

    private void AnimateHeart(string name)
    {
        if (name == "Arrow_Left")
        {
            animator.Play("Base Layer.Heart_left");
            //animator.SetInteger("Color", 1);
        }
        else if (name == "Arrow_Right")
        {
            animator.Play("Base Layer.Heart_Right");
            //animator.SetInteger("Color", 2);
        }
        else if (name == "Arrow_Up")
        {
            animator.Play("Base Layer.Heart_Up");
            //animator.SetInteger("Color", 3);
        }
        else if (name == "Arrow_Down")
        {
            animator.Play("Base Layer.Heart_Down");
            //animator.SetInteger("Color", 4);
        }
    }

    private void ChoosePattern()
    {
        paused = true;
        StartCoroutine("Pause");
        //mat = arrowsList[rand].GetComponent<Renderer>().material;
        //mat.EnableKeyword("_Emission");
    }

    IEnumerator Pause()
    {
        //turn off
        foreach (GameObject obj in arrowsList)
        {
            mat = obj.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");
        }
        yield return new WaitForSeconds(0.75f);
        paused = false;

        //randomly choose arrow for player to press
        rand = Random.Range(0, 4);
        //have to do this stupid for loop because using arrowsList[rand] as the object doesn't work
        foreach (GameObject obj in arrowsList)
        {
            if (arrowsList[rand].name == obj.name)
            {
                mat = obj.GetComponent<Renderer>().material;
                mat.EnableKeyword("_EMISSION");
            }
        }
    }

    IEnumerator Flash ()
    {
        print("Incorrect input");
        //flash off
        foreach (GameObject obj in arrowsList)
        {
            mat = obj.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");
        }
        //wait
        yield return new WaitForSeconds(0.25f);
        //flash on
        foreach (GameObject obj in arrowsList)
        {
            mat = obj.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");
        }
        //wait
        yield return new WaitForSeconds(0.25f);
        //flash off
        foreach (GameObject obj in arrowsList)
        {
            mat = obj.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");
        }
        //wait
        yield return new WaitForSeconds(0.25f);
        //flash on
        foreach (GameObject obj in arrowsList)
        {
            mat = obj.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");
        }
        //wait
        yield return new WaitForSeconds(0.25f);
        //turn off
        foreach (GameObject obj in arrowsList)
        {
            mat = obj.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");
        }

        flashing = false;
        count = 0;
        ChoosePattern();

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (HeartColor.text != "green" && !flashing && !paused)
        {
            if (coll.gameObject.tag == "Player")
            {
                //turn on all arrows if correct non-center tile is touched
                if (started == true && tileList[rand].name == gameObject.name && gameObject.name != "Center")
                {
                    //have to do this stupid for loop because using arrowsList[rand] as the object doesn't work
                    foreach (GameObject obj in arrowsList)
                    {
                        if (arrowsList[rand].name == obj.name)
                        {
                            mat = obj.GetComponent<Renderer>().material;
                            mat.DisableKeyword("_EMISSION");

                            AnimateHeart(obj.name);
                        }
                    }

                    if (count == 2)
                    {
                        DanceComplete.Invoke();
                        count = 0;
                        //if heart is still not fully fixed, do another pattern
                        if(HeartColor.text != "green")
                        {
                            //create new pattern
                            ChoosePattern();
                        //if fully fixed, change to not broken and reset to all arrows to on
                        } else
                        {
                            animator.SetInteger("Color", 5);
                            foreach (GameObject obj in arrowsList)
                            {
                                mat = obj.GetComponent<Renderer>().material;
                                mat.EnableKeyword("_EMISSION");
                            }
                            started = false;
                            ChangeBroken();
                        }
                    }
                    else
                    {
                        ++count;
                        ChoosePattern();
                    }
                //flash arrows and restart if incorrect non-center tile is touched
                }
                else if (started == true && tileList[rand] != gameObject && gameObject.name != "Center")
                {
                    flashing = true;
                    StartCoroutine("Flash");
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
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.name == "Player" && gameObject.name != "Center")
        {
            /*if(tileList[rand] == gameObject)
            {
                arrowsList[rand].SetActive(false);
            }*/
        }

    }
}
