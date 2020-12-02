using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	//start with zero coins
	public int points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //checks to see if picked up object, activated everytime touch a trigger collider
    void OnTriggerEnter(Collider other) 
    {
    	//test tag, if string is same as pick up...
    	if (other.gameObject.CompareTag("Pick Up"))
    	{
    		//deactivates game object
    		other.gameObject.SetActive(false);
    	}
    }
    
    private void OnGUI(){
    	GUI.Label(new Rect(10, 10, 100, 20), "Bananas : " + points);
    }

}

