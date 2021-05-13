using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Used this tutorial for laser beam
//https://www.youtube.com/watch?v=pNE3rfMGEAw&ab_channel=Doc
public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;
    private bool hitTarget = false;
    public UnityEvent LaserCorrect;
    public Text starColor;
    private bool starFixed = true;
    GameObject mirror1;
    GameObject mirror2;
    GameObject mirror3;

    // Update is called once per frame
    void Update()
    {
        if(starColor.text != "green")
        {
            Destroy(GameObject.Find("Laser Beam"));
            beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, hitTarget);
            if (beam.targetHit == true)
            {
                LaserCorrect.Invoke();
                //Debug.Log("WEEEE");
            }
        } else if(GameObject.Find("Laser Beam") != null)
        {
            StartCoroutine("DestroyLaser");
        }
    }

    public void ShuffleMirror()
    {
        mirror1 = GameObject.Find("MirrorCart/Rail");
        mirror2 = GameObject.Find("MirrorCart/Rail (1)");
        mirror3 = GameObject.Find("MirrorCart/Rail (2)");
        GameObject child1 = mirror1.transform.GetChild(1).gameObject;
        GameObject child2 = mirror2.transform.GetChild(1).gameObject;
        GameObject child3 = mirror3.transform.GetChild(1).gameObject;

        float RandZ1 = Random.Range(child1.transform.GetChild(0).gameObject.transform.position.z, child1.transform.GetChild(1).gameObject.transform.position.z);
        float RandZ2 = Random.Range(child2.transform.GetChild(0).gameObject.transform.position.z, child2.transform.GetChild(1).gameObject.transform.position.z);
        float RandZ3 = Random.Range(child3.transform.GetChild(0).gameObject.transform.position.z, child3.transform.GetChild(1).gameObject.transform.position.z);

        mirror1.transform.GetChild(0).gameObject.transform.position = new Vector3(mirror1.transform.GetChild(0).gameObject.transform.position.x, mirror1.transform.GetChild(0).gameObject.transform.position.y, RandZ1);
        mirror2.transform.GetChild(0).gameObject.transform.position = new Vector3(mirror2.transform.GetChild(0).gameObject.transform.position.x, mirror2.transform.GetChild(0).gameObject.transform.position.y, RandZ2);
        mirror3.transform.GetChild(0).gameObject.transform.position = new Vector3(mirror3.transform.GetChild(0).gameObject.transform.position.x, mirror3.transform.GetChild(0).gameObject.transform.position.y, RandZ3);
    }

    IEnumerator DestroyLaser ()
    {
        yield return new WaitForSeconds(1f);
        Destroy(GameObject.Find("Laser Beam"));
    }
}
