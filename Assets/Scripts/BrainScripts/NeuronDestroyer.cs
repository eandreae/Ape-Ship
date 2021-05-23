using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NeuronDestroyer : MonoBehaviour
{
    public UnityEvent OnNeuronDeposit;
    // Start is called before the first frame update
    void Start()
    {
        
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
        Destroy(this.gameObject);
        //fix the brain
        OnNeuronDeposit.Invoke();
    }
}
