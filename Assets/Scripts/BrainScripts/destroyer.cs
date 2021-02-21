using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class destroyer : MonoBehaviour
{
    public float lifetime = 5f;
    public Text nodeColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destruction();
            }
        }*/
    }
    //if object enters destroy area
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "neuronDestroyer")
        {
            if (this.gameObject.name == "neuronSpawn(Clone)")
            {
                Debug.Log("YEEE");

                Destruction();
            }
        }
        else if (coll.gameObject.name == "foodDestroyer")
        {
            if (this.gameObject.name == "foodSpawn(Clone)")
            {
                Destruction();
            }
        }

    }
    //destory object
    void Destruction()
    {
        Destroy(this.gameObject);
        //fix the brain
        if (nodeColor.text == "red")
            nodeColor.text = "yellow";
        else if (nodeColor.text == "yellow")
            nodeColor.text = "green";
    }
}
