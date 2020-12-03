using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElecChangeInstance : MonoBehaviour
{
    [SerializeField] private Renderer myObject;
    int count = 0;

    public Text color;
    private void Start()
    {
        if(color.text == "green")
        {
            myObject.material.color = Color.green;
        } else if(color.text == "yellow")
        {
            myObject.material.color = Color.yellow;
        } else if (color.text == "red")
        {
            myObject.material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(color.text == "yellow")
            {
                myObject.material.color = Color.green;
                color.text = "green";
            } else if(color.text == "red")
            {
                myObject.material.color = Color.yellow;
            }
        } else if(other.CompareTag("Monkey"))
        {
            if (color.text == "yellow")
            {
                myObject.material.color = Color.red;
                color.text = "red";
            } else if(color.text == "green")
            {
                myObject.material.color = Color.red;
                color.text = "red";
            }
        }
    }
}
