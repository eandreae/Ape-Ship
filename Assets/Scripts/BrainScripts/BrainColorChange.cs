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
    Color color4;
    Color color5;
    int index;
    int bound;
    bool swapColor = true;
    List<Color> ColorList = new List<Color>();

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
        ColorList.Add(color1);
        color2 = materials[1].color;
        ColorList.Add(color2);
        color3 = materials[2].color;
        ColorList.Add(color3);
        color4 = materials[3].color;
        ColorList.Add(color4);
        if (materials.Length == 5)
        {
            color5 = materials[4].color;
            ColorList.Add(color5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (brainColor.text == "yellow")
        {
            materials[0].color = materials[0].color * (1.05f);
            //materials[0].color.g = materials[0].color.g * (0.3f);
            //materials[0].color.b = materials[0].color.b * (0.3f);


        }
        else if (brainColor.text == "red")
        {
            materials[0].color = materials[0].color * (1.05f);
            materials[1].color = materials[1].color * (1.05f);
            materials[2].color = materials[2].color * (1.05f);
        } else //brainColor == GREEN
        {
            //materials[0].color = color1;
            //materials[1].color = color2;
            //materials[2].color = color3;
            if (swapColor)
            {
                swapColor = false;
                StartCoroutine("ColorSwap");
            }
        }
    }

    IEnumerator ColorSwap()
    {
        while(brainColor.text == "green")
        {
            yield return new WaitForSeconds(2f); // time in seconds to wait
            if(ColorList.Count == 5)
            {
                bound = 5;

            } else
            {
                bound = 4;
            }
            //pick random color
            index = Random.Range(0, bound);

            for (int i = 0; i < ColorList.Count; ++i)
            {
                if(index >= bound)
                {
                    index = 0;
                }
                materials[index].color = ColorList[i];
                ++index;
            }
        }
        swapColor = true;
    }

}
