//Use instead of Player script right to fix bugs
//Referenced through Aaron Hibberd's video on player movement: https://www.youtube.com/watch?v=sXQI_0ILEW4

//Using BeepBoopIndie's video on collecting coins: https://www.youtube.com/watch?v=XnKKaL5iwDM
//Also used Unity's official tutorial on collecting objects: https://learn.unity.com/tutorial/collecting-scoring-and-building-the-game?projectId=5c51479fedbc2a001fd5bb9f#5c7f8529edbc2a002053b78a


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
    	moveSpeed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        //checks to see if pressing any arrow keys
        //if so will go horizontal if left or right
        //will go vertical if up or down
        	transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);
        
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
