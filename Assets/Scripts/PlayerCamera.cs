// Controls the movement of the Camera that follows the Player.
// Referenced https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0, 19f, -10);

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.rotation = Quaternion.Euler(52.22f,0f,0f);
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition;
    }
}
