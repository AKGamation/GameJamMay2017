using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonsingle : MonoBehaviour {
    //give this a reference to the door
    public GameObject DoorToControl;
    public GameObject OtherDoor;



    public void toggledoor()
    {
        bool DoorOpen = DoorToControl.GetComponent<Doorbasic>().open;

        if (DoorOpen == true)
        {
            DoorToControl.GetComponent<Doorbasic>().open = false;
        }
        else
        {
            DoorToControl.GetComponent<Doorbasic>().open = true;
        }
    }

    public void toggleMulti()
    {
        bool DoorOpen = DoorToControl.GetComponent<Doorbasic>().open;
        bool OtherDoorOpen = OtherDoor.GetComponent<Doorbasic>().open;
        if (DoorOpen == true)
        {
            DoorToControl.GetComponent<Doorbasic>().open = false;
            OtherDoor.GetComponent<Doorbasic>().open = false;
        }
        else if(DoorOpen == false)
        {
            DoorToControl.GetComponent<Doorbasic>().open = true;
            OtherDoor.GetComponent<Doorbasic>().open = true;
        }
        
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
