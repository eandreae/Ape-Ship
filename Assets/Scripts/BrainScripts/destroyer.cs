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
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destruction();
            }
        }*/
    }
    //if object enters destroy area
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "neuronDestroyer")
        {
            if (this.gameObject.name == "neuronSpawn(Clone)")
            {
                Destruction();
            }
        }
        else if (coll.gameObject.name == "foodDestroyer")
        {
            if (this.gameObject.name == "KrillSandwich(Clone)")
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
