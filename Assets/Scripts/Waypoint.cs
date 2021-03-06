using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Transform[] waypoints;

    Transform focus;

    public void WhichWaypoint(int wp)
    {
        focus = waypoints[wp];
    }

    // Update is called once per frame
    void Update()
    {
        if (focus != null){
            transform.LookAt(focus);
        }
    }
}
