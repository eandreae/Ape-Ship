using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ItemScript : MonoBehaviour
{
    public bool pickedUp;
    public bool active;
    public bool thrown;
    public Transform playerRoot;
    //private GameObject[] playerObjs;
    public string type;
    private float height;
    //private Rigidbody rigidbody;
    //public GameObject glowEffect;
    public Waypoint wp;
    NetworkManager nm;

    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
        active = false;
        thrown = false;
        //playerRoot = GameObject.FindWithTag("PlayerRoot").GetComponent<Transform>();
        //this.rigidbody = this.GetComponent<Rigidbody>();
        //this.rigidbody.isKinematic = false;
        //this.rigidbody.isKinematic = true;
        this.height = this.transform.position.y;
        nm = GameObject.FindObjectOfType<NetworkManager>();

        //if (this.glowEffect)
        //    this.glowEffect.SetActive(false);

    }

    // Update is called once per frame
    public void Update()
    {
        if(!pickedUp){ // code to execute if object is not picked up
            //this.rigidbody.isKinematic = false;

            //if (this.type == "Banana" || this.type == "Coin"){
            //transform.Rotate(0, 0, 90 * Time.deltaTime);
            //}
            this.playerRoot = null;
        }
        else {  // code to execute if object is picked up
            //if(this.glowEffect)
            //    this.glowEffect.SetActive(false);
            this.active = false;
            this.GetComponent<Rigidbody>().isKinematic = true; // if picked up, item become kinematic
            //Debug.Log(playerRoot.position);

            transform.position = playerRoot.position + 1.2f * (playerRoot.forward) + new Vector3(0, 2f, 0); // sets position relative to the player transform

            if (type == "Neuron"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, -90); // keep rotation at a constant value
                Waypoint.WhichWaypoint(0);
            }
            else if(type == "Banana"){
                transform.rotation = playerRoot.rotation *  Quaternion.Euler(-90, -90, 0); // keep rotation at a constant value
                Waypoint.WhichWaypoint(1);
            } 
            else if(type == "Canister1"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 0, 90); // keep rotation at a constant value
                Waypoint.WhichWaypoint(2);
                AlterSpeed(6f);
            }
            else if(type == "Canister2"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 0, 90); // keep rotation at a constant value
                Waypoint.WhichWaypoint(3);
                AlterSpeed(6f);
            }
            else if (type == "Sandwich"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
                Waypoint.WhichWaypoint(4);
            }
            else if (type == "Kebab"){
                transform.position -= new Vector3(0, 2f, 0); // sets position relative to the player transform
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
                Waypoint.WhichWaypoint(4);
            }
            else if (type == "Nuke"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
            }
            else if (type == "Battery")
            {
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
                AlterSpeed(4f);
            }

        }
    }

    private void OnTriggerEnter(Collider other) {
    	if (other.tag == "Player") {
    		//other.GetComponent<Player>().points++;
        	//Add 1 to points.
        	//Destroy(gameObject); //Destroys coin, when touched.
            //this.playerRoot = other.gameObject.GetComponent<Transform>(); // save the player transform (use this in case of multiple playerobjects)
            this.GetComponent<Rigidbody>().isKinematic = false;
            //if (this.glowEffect && !this.pickedUp && !playerRoot.GetComponent<Player>().holding){
            //    this.glowEffect.SetActive(true);
            //}
        }
        else if (this.active) {
            if (this.type == "Banana" && other.tag == "Gorilla" && !this.pickedUp){
                Object.Destroy(this.gameObject, 0.25f); // destroy object after contact with gorilla
            }
            else if (this.thrown && this.type == "Nuke" && other.tag == "Gorilla" && !this.pickedUp){
                Object.Destroy(this.gameObject, 0.52f); // destroy object after contact with gorilla
            }
        }
    }

    public void AlterSpeed(float newSpeed)
    {
        if(nm)
            playerRoot.GetComponent<Player>().ChangeSpeed(newSpeed);
        else 
            playerRoot.GetComponent<Player1P>().ChangeSpeed(newSpeed);
    }

    private void OnTriggerExit(Collider other) {
    	if (other.tag == "Player"){
            if (!this.thrown){  
                //this.rigidbody.isKinematic = true;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            // else {
            //     StartCoroutine("ThrownPhysics"); // set object to kinematic 
            // }
        }
        
    }

}
