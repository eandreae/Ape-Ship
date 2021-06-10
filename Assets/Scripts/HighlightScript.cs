using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    public GameObject highlight;
    // Start is called before the first frame update

    public void highlightOn() {
        highlight.SetActive(true);
    }

    public void highlightOff() {
        highlight.SetActive(false);
    }

}
