using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecChangeInstance : MonoBehaviour
{
    [SerializeField] private Renderer myObject;
    int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            count += 1;
            if (count == 1)
            {
                myObject.material.color = Color.yellow;
            } else if(count == 2)
            {
                myObject.material.color = Color.green;
            } else if (count == 3)
            {
                myObject.material.color = Color.red;
                count = 0;
            }
        }
    }
}
