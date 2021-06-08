using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using Mirror;

public class destroyer : MonoBehaviour
{
    public NetworkManager nm;
    public float lifetime = 5f;
    private float startingLife;
    public Text nodeColor;
    public NodeInstanceManager selfNode;
    //public UnityEvent OnNeuronDeposit;

    // Start is called before the first frame update
    void Start()
    {
        nm = GameObject.FindObjectOfType<NetworkManager>();
        startingLife = lifetime;

        if (nm){
            if (this.GetComponent<ItemScript>().type == "Canister1"){
                this.nodeColor = GameObject.Find("OxygenColor").GetComponent<Text>();
                selfNode = GameObject.Find("LungTarget").GetComponent<NodeInstanceManager>();
            }
            else if (this.GetComponent<ItemScript>().type == "Canister2" ) {
                this.nodeColor = GameObject.Find("Oxygen2Color").GetComponent<Text>();
                selfNode = GameObject.Find("Lungs_2Target").GetComponent<NodeInstanceManager>();
            }
            else {
                this.nodeColor = GameObject.Find("StomachColor").GetComponent<Text>();
                selfNode = GameObject.Find("StomachTarget").GetComponent<NodeInstanceManager>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(nm){
            if (lifetime > 0 && !(this.GetComponent<ItemScript>().type == "Canister1"
                            || this.GetComponent<ItemScript>().type == "Canister2"))
            {
                lifetime -= Time.deltaTime;
                if (lifetime <= 0 && !this.gameObject.GetComponent<ItemScript>().pickedUp)
                {
                    Destroy(this.gameObject);
                } else if (this.gameObject.GetComponent<ItemScript>().pickedUp)
                {
                    lifetime = startingLife;
                }
            }
        }
        else{
                if (lifetime > 0 && !(this.GetComponent<ItemScript1P>().type == "Canister1"
                            || this.GetComponent<ItemScript1P>().type == "Canister2"))
            {
                lifetime -= Time.deltaTime;
                if (lifetime <= 0 && !this.gameObject.GetComponent<ItemScript1P>().pickedUp)
                {
                    Destroy(this.gameObject);
                } else if (this.gameObject.GetComponent<ItemScript1P>().pickedUp)
                {
                    lifetime = startingLife;
                }
            }
        }
    }
    //if object enters destroy area
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "neuronDestroyer")
        {
            if (this.gameObject.name == "neuronSpawn(Clone)")
            {
                //Destruction();
            }
        }
        else if (coll.gameObject.name == "foodDestroyer" && nodeColor.text != "green")
        {
            if (this.gameObject.name == "networkSandwich(Clone)" || 
                this.gameObject.name == "networkKebab(Clone)" || 
                this.gameObject.name == "networkBanana(Clone)" ||
                this.gameObject.name == "KrillSandwich (1P)(Clone)" || 
                this.gameObject.name == "SeaFoodKebab (1P)(Clone)" || 
                this.gameObject.name == "Banana (1P)(Clone)"
                )
            {
                Destruction();
            }
        } else if (coll.gameObject.name == "canisterDestroyer")
        {
            if (this.gameObject.name == "Air_Tank_3(Clone)" || this.gameObject.name == "1p Air_Tank_3(Clone)" )
            {
                Destruction();
            }
        } else if (coll.gameObject.name == "canisterDestroyer(O1)")
        {
            if (this.gameObject.name == "Air_Tank_3(O1)(Clone)" || this.gameObject.name == "1p Air_Tank_3(O1)(Clone)" )
            {
                Destruction();
            }
        }

    }
    //destory object
    void Destruction()
    {
        Destroy(this.gameObject);
        //fix the brain
        //OnNeuronDeposit.Invoke();
        selfNode.FixNode();
    }
}
