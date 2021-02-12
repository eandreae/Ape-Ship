using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float backgroundSpeed;

    Renderer backgroundRend;

    // Start is called before the first frame update
    void Start()
    {
        backgroundRend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        backgroundRend.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}
