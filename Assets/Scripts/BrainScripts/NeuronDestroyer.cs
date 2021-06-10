using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class NeuronDestroyer : MonoBehaviour
{
    public UnityEvent OnNeuronDeposit;
    public NetworkManager nm;
    public NodeInstanceManager selfNode;
    public spawner BrainSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        nm = GameObject.FindObjectOfType<NetworkManager>();
        if(nm){
            selfNode = GameObject.FindGameObjectWithTag("Nav").GetComponent<NodeInstanceManager>();
            spawner = GameObject.Find("BrainTerminal2").GetComponent<spawner>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "neuronDestroyerBlue" && this.gameObject.name == "neuronSpawnBlue(Clone)")
        {
            Destruction();
        }
        
        if (coll.gameObject.name == "neuronDestroyerRed" && this.gameObject.name == "neuronSpawnRed(Clone)")
        {
            Destruction();
        }

        if (coll.gameObject.name == "neuronDestroyerGreen" && this.gameObject.name == "neuronSpawnGreen(Clone)")
        {
            Destruction();
        }
    }

    void Destruction()
    {
        //fix the brain
        if(nm){
            BrainSpawner.Deposit();
        }else{
            OnNeuronDeposit.Invoke();
        }
        Destroy(this.gameObject);
    }
}
