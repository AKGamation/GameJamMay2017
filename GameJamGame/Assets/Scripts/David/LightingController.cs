using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour {


    public GameObject button;
    public bool lightState = true;

    //need event from button or sensor

    
    
    
	// Use this for initialization
	void Start ()
    {
    //   name of button script
	//   button.GetComponent<buttscrnam>	
	}
	
	// Update is called once per frame
	void Update () {


        //if(button.buttonscript == true)
        //{
        //  lightstate = true;
        //  GetComponent<Light>().enabled = false;
        //{
        //else
        //{
        //  lightstate = false
        //  GetComponent<Light>().enabled = true;
        //{

        //testing
        if(lightState == false)
        {
            GetComponent<Light>().enabled = false;
        }
        else if(lightState == true)
        {
            GetComponent<Light>().enabled = true;
        }
       


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
