using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Used this tutorial for laser beam
//https://www.youtube.com/watch?v=pNE3rfMGEAw&ab_channel=Doc
public class LaserBeam: MonoBehaviour
{
    Vector3 pos, dir;
    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();
    public bool targetHit;

    public LaserBeam(Vector3 pos, Vector3 dir, Material material, bool targetHit)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;
        this.targetHit = targetHit;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.cyan;
        this.laser.endColor = Color.cyan;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 300, 1))
        {
            CheckHit(hit, dir, laser);
        } else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach(Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++; 
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if (hitInfo.collider.gameObject.tag == "Reciever")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, laser);

            this.targetHit = true;
        }

        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }

    }

}
