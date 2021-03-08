﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public static Transform[] waypoints;
    public static Transform focus;

    public void Start(){
        waypoints = new Transform[4];
        waypoints[0] = GameObject.Find("NeuronWaypoint").GetComponent<Transform>();
        waypoints[1] = GameObject.Find("BananaWaypoint").GetComponent<Transform>();
        waypoints[2] = GameObject.Find("CanisterWaypoint").GetComponent<Transform>();
        waypoints[3] = GameObject.Find("KrillWaypoint").GetComponent<Transform>();
        Debug.Log(waypoints);
    }

    public static void WhichWaypoint(int wp)
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
