using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BrainColorChange : MonoBehaviour
{
    GameObject brainObj;
    public Text brainColor;
    Material[] materials;
    Color color1;
    Color color2;
    Color color3;

    // Start is called before the first frame update
    void Start()
    {
        //FILE LOCATION ASSETS/RESOURCES/MATERIALS
        brainObj = GameObject.FindGameObjectWithTag("Nav");
        materials = GetComponent<Renderer>().materials;
        //Debug.Log(materials[0].color);
        //Debug.Log(materials[1].color);
        //Debug.Log(materials[2].color);
        //Debug.Log(materials[3].color);
        //Debug.Log(materials[4].color);
        color1 = materials[0].color;
        color2 = materials[1].color;
        color3 = materials[2].color;

    }

    // Update is called once per frame
    void Update()
    {
        if (brainColor.text == "yellow")
        {
            materials[0].color = materials[0].color * (0.95f);
            //materials[0].color.g = materials[0].color.g * (0.3f);
            //materials[0].color.b = materials[0].color.b * (0.3f);


        }
        else if (brainColor.text == "red")
        {
            materials[0].color = materials[0].color * (0.95f);
            materials[1].color = materials[1].color * (0.95f);
            materials[2].color = materials[2].color * (0.95f);
        } else
        {
            materials[0].color = color1;
            materials[1].color = color2;
            materials[2].color = color3;

        }
    }

}
