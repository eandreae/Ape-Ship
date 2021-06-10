using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class remove1P : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SelfDestruct", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindObjectOfType<NetworkManager>())
            Destroy(this.gameObject);
    }

    IEnumerator SelfDestruct(float time){
        yield return new WaitForSeconds(time);
        Destroy(this); // destroy this script, not the gameobject
    }
}
