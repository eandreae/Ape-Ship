using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public bool pickedUp;
    private Transform playerRoot;
    private GameObject[] playerObjs;
    public string type;
    private float height;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
        playerRoot = GameObject.FindWithTag("PlayerRoot").GetComponent<Transform>();
        this.rigidbody = this.GetComponent<Rigidbody>();
        this.height = this.transform.position.y;
        playerObjs = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject p in playerObjs){
            Debug.Log(p.GetComponent<Collider>());
            Debug.Log(this.GetComponent<BoxCollider>());
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), p.GetComponent<Collider>(), true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickedUp){ // spin; apply gravity
            this.rigidbody.isKinematic = false;
            
            if (this.type == "Banana" || this.type == "Coin"){
                transform.Rotate(0, 0, 90 * Time.deltaTime);
            }

        }
        else {
            this.rigidbody.isKinematic = true;

            if(type == "Coin"){
                transform.localRotation = Quaternion.Euler(0, 90, -90); // keep rotation at a constant value
            }
            
            else if(type == "Banana"){
                transform.localRotation = Quaternion.Euler(-90, -90, 0); // keep rotation at a constant value
            }
            
            //Debug.Log(playerRoot.position);
            transform.localPosition = new Vector3(0.0f, playerRoot.position.y + 1.0f, 0.4f); 
        }
    }

    private void OnTriggerEnter(Collider other) {
    	if(other.tag == "Player") {
    		//other.GetComponent<Player>().points++;
        	//Add 1 to points.
        	//Destroy(gameObject); //Destroys coin, when touched.
            this.playerRoot = other.gameObject.GetComponent<Transform>();
        }
        if(other.tag == "Gorilla" && !this.pickedUp){
            Object.Destroy(this.gameObject, 0.5f); // destroy object after contact with gorilla
        }
        if(this.tag == "Banana" && other.tag == "Stomach" && !this.pickedUp){
            Object.Destroy(this.gameObject); // destroy when touching the stomach?
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
