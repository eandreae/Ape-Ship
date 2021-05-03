using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private string hitting = "";
    private bool reflecting = false;
    private bool reset = false;
    private float scalar = 0.1f;
    private Vector3 defaultPosition;
    private Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = gameObject.transform.position;
        defaultScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset == true)
        {

        }
        //if not currently hitting wall, move and scale until wall is hit
        if (hitting == "Mirror")
        {
            gameObject.transform.position = defaultPosition;
            gameObject.transform.localScale = defaultScale;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x - scalar, gameObject.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y + scalar, gameObject.transform.localScale.z);
            reflecting = true;
        }
        else if (hitting == "Wall" && reflecting == false)
        {
            hitting = "Wall";
        } else if(hitting == "")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - scalar, gameObject.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y + scalar, gameObject.transform.localScale.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mirror")
        {
            reset = true;
            hitting = "Mirror";
        } else if(other.tag == "Walls" && reflecting == false)
        {
            hitting = "Wall";
        }
    }

    private void Reflect()
    {
        GameObject temp = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        temp.transform.Rotate(45.0f, 0f, 0f);
    }
}