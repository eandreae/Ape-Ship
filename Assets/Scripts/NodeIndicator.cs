using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeIndicator : MonoBehaviour
{
    public GameObject indicatorObj;
    public Material greenMat;
    public Material yellowMat;
    public Material redMat;
    public Material greyMat;

    // Start is called before the first frame update
    void Start()
    {
        SetGreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGreen() {
        indicatorObj.GetComponent<Renderer>().material = greenMat;
    }

    public void SetYellow() {
        indicatorObj.GetComponent<Renderer>().material = yellowMat;
    }

    public void SetRed() {
        indicatorObj.GetComponent<Renderer>().material = redMat;
    }

    public void SetGrey() {
        indicatorObj.GetComponent<Renderer>().material = greyMat;
    }
}
