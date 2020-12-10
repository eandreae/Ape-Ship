using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElecChangeInstance : MonoBehaviour
{
    [SerializeField] private Renderer myObject;

    public Text color;
    public Image display;
    private void Start()
    {
        if(color.text == "green")
        {
            myObject.material.color = Color.green;
            display.color = Color.green;
        } else if(color.text == "yellow")
        {
            myObject.material.color = Color.yellow;
            display.color = Color.yellow;
        } else if (color.text == "red")
        {
            myObject.material.color = Color.red;
            display.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(color.text == "yellow")
            {
                myObject.material.color = Color.green;
                display.color = Color.green;
                color.text = "green";
            } else if(color.text == "red")
            {
                myObject.material.color = Color.yellow;
                display.color = Color.yellow;
                color.text = "yellow";
            }
        } else if(other.CompareTag("Monkey"))
        {
            if (color.text == "yellow")
            {
                myObject.material.color = Color.red;
                display.color = Color.red;
                color.text = "red";
            } else if(color.text == "green")
            {
                myObject.material.color = Color.red;
                display.color = Color.red;
                color.text = "red";
            }
        }
    }
}
