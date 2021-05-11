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
    GameObject mirror;

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
        mirror = GameObject.Find("MirrorCart");
        float RandZ = Random.Range(-9.0f, 6.0f);
        Debug.Log("Found");
        mirror.transform.position = new Vector3(mirror.transform.position.x, mirror.transform.position.y, mirror.transform.position.z + RandZ);
    }

    IEnumerator DestroyLaser ()
    {
        yield return new WaitForSeconds(1f);
        Destroy(GameObject.Find("Laser Beam"));
    }
}
