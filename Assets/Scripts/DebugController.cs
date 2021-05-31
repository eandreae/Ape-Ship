using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugController : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent Execute;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")) {
            Debug.Log("Debug Activated");
            Execute.Invoke();
        }
    }
}
