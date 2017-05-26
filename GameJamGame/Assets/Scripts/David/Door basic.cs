using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorbasic : MonoBehaviour {

    public GameObject OpenPos;
    public GameObject ClosePos;
    public float speed;
    bool open;
    float step;
    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        DoorMove();

    }

    private void DoorMove()
    {
        if (open == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, OpenPos.transform.position, step);
            
        }
        else if(open == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, ClosePos.transform.position, step);
        }


    }


}