using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElecChangeInstance : MonoBehaviour
{
    [SerializeField] private Renderer myObject;
    int count = 0;

    public Text color;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            count += 1;
            if (count == 1)
            {
                myObject.material.color = Color.yellow;
                color.text = "yellow";
            } else if(count == 2)
            {
                myObject.material.color = Color.green;
                color.text = "green";
            } else if (count == 3)
            {
                myObject.material.color = Color.red;
                color.text = "red";
                count = 0;
            }
        }
    }
}
