//Using BeepBoopIndie's video on collecting coins: https://www.youtube.com/watch?v=XnKKaL5iwDM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public bool pickedUp;
    private Transform playerRoot;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
        playerRoot = GameObject.FindWithTag("PlayerRoot").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickedUp){
            if(type == "Coin"){
                transform.Rotate(90 * Time.deltaTime, 0, 0);
            }
            
            else if(type == "Banana"){
                transform.Rotate(0, 0, 90 * Time.deltaTime);
            }

            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }
        else {
            if(type == "Coin"){
                transform.localRotation = Quaternion.Euler(0, 90, -90); // keep rotation at a constant value
            }
            else if(type == "Banana"){
                transform.localRotation = Quaternion.Euler(-90, -90, 0); // keep rotation at a constant value
            }
            //Debug.Log(playerRoot.position);
            transform.localPosition = new Vector3(playerRoot.localPosition.x, 
                                                  playerRoot.position.y / 2.2f, 
                                                  0.4f); 
        }
    }

    // private void OnTriggerEnter(Collider other) {
    // 	if(other.name == "Capsule" || other.name == "Player") {
    // 		other.GetComponent<Player>().points++;
    //     	//Add 1 to points.
    //     	Destroy(gameObject); //Destroys coin, when touched.
    //     }
    // }
}