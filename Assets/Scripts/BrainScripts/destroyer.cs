using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;

public class destroyer : MonoBehaviour
{
    public float lifetime = 5f;
    public Text nodeColor;
    public UnityEvent OnNeuronDeposit;

    // Start is called before the first frame update
    void Start()
    {
        if(!nodeColor){
            if(this.GetComponent<ItemScript>().type == "Neuron" || this.GetComponent<ItemScript>().type == "Canister2" ){
                this.nodeColor = GameObject.Find("NavigationColor").GetComponent<Text>();
            }
            else if(this.GetComponent<ItemScript>().type == "Canister1" || this.GetComponent<ItemScript>().type == "Canister2" ){
                this.nodeColor = GameObject.Find("OxygenColor").GetComponent<Text>();
            }
            else {
                this.nodeColor = GameObject.Find("StomachColor").GetComponent<Text>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destroy(this.gameObject);
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
            if (this.gameObject.name == "Air_Tank_3(Clone)")
            {
                Destruction();
            }
        } else if (coll.gameObject.name == "canisterDestroyer(O1)")
        {
            if (this.gameObject.name == "Air_Tank_3(O1)(Clone)")
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
        OnNeuronDeposit.Invoke();
    }
}
