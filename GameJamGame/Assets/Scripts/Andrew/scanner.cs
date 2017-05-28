using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanner : MonoBehaviour
{
    public bool activated = false;
    public GameObject doorToOpen;
	
	// Update is called once per frame
	void Update ()
    {
		if(activated)
        {
            doorToOpen.GetComponent<Doorbasic>().toggleDoor();
        }
	}
}
