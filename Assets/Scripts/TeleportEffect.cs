using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    private Renderer rend;
    private float startval = -5.0f;
    private float endval = 4.0f;
    Escape canTeleport;
    GameObject escapeObj;

    // Start is called before the first frame update
    void Start()
    {
        escapeObj = GameObject.Find("Escape");
        canTeleport = escapeObj.GetComponent<Escape>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canTeleport.teleport == true)
        {
            rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Shader Graphs/URP Dissolve/Dissolve_Direction_Unlit_DoubleSide");

            int ID = Shader.PropertyToID("EdgeWidth");
            rend.material.SetFloat("_DierctionEdgeWidthScale", startval);

            startval = Mathf.Lerp(startval, endval, Time.deltaTime * 0.2f);
        }
    }
}
