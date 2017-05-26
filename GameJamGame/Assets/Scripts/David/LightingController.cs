using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour {


    public GameObject button;
    public bool lightState;

    //need event from button or sensor

    
    
    
	// Use this for initialization
	void Start ()
    {
    //   name of button script
	//   button.GetComponent<buttscrnam>	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToggleLight()
    {
        if (lightState == true)
        {
            lightState = false;
        }
        else
        {
            lightState = true;
        }
    }





}
