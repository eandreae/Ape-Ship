using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimateTeleport : MonoBehaviour
{
    private Renderer rend;
    private float startval = 0.5f;
    private float endval = -0.5f;
    private float defaultstart;
    bool beam = false;
    private bool swap = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultstart = startval;
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Shader Graphs/URP Dissolve/Dissolve_Direction_Unlit_DoubleSide");

        StartCoroutine("BeamUp");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(beam == true)
        {
            ChangeRender();
        }
    }

    private void ChangeRender()
    {
        if (!swap)
        {
            int ID = Shader.PropertyToID("EdgeWidth");
            rend.material.SetFloat("_DierctionEdgeWidthScale", startval);

            startval = Mathf.Lerp(startval, endval, Time.deltaTime * 1.5f);
        } else
        {
            int ID = Shader.PropertyToID("EdgeWidth");
            rend.material.SetFloat("_DierctionEdgeWidthScale", startval);

            startval = Mathf.Lerp(startval, defaultstart, Time.deltaTime * 1.5f);

        }
        if (startval < (0.9 * endval))
        {
            swap = true;
        } else if(swap && startval > (0.4 * defaultstart))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator BeamUp()
    {
        yield return new WaitForSeconds(3.75f);
        beam = true;
    }
}
