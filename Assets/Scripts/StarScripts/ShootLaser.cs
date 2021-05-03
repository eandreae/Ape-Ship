using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used this tutorial for laser beam
//https://www.youtube.com/watch?v=pNE3rfMGEAw&ab_channel=Doc
public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Laser Beam"));
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
