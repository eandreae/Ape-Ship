//Using BeepBoopIndie's video on collecting coins: https://www.youtube.com/watch?v=XnKKaL5iwDM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public bool pickedUp;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickedUp)
            transform.Rotate(90 * Time.deltaTime, 0, 0);
    }

    // private void OnTriggerEnter(Collider other) {
    // 	if(other.name == "Capsule" || other.name == "Player") {
    // 		other.GetComponent<Player>().points++;
    //     	//Add 1 to points.
    //     	Destroy(gameObject); //Destroys coin, when touched.
    //     }
    // }
}