using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonsingle : MonoBehaviour {
    //give this a reference to the door
    public GameObject DoorToControl;
    public GameObject OtherDoor;



    public void toggledoor()
    {
        

        
          DoorToControl.GetComponent<Doorbasic>().toggleDoor();
        
        
    }

    public void toggleMulti()
    {
        

        DoorToControl.GetComponent<Doorbasic>().toggleDoor();
        OtherDoor.GetComponent<Doorbasic>().toggleDoor();

        
        
    }

        
    


    //andrew's code for the most part


    // called while the button is not being interacted with
    public void onIdle()
    {

    }

    // called while the button is being hovered
    public void onHover()
    {

    }

    // called once when the button is first pressed
    public void onPress()
    {

        if(OtherDoor != null)
        {
            toggleMulti();
        }
        else
        {
            toggledoor();

        }

    }

    // called when the button is held
    public void onHold()
    {

    }

    // called once when the button stops being pressed
    public void onRelease()
    {

    }
}
