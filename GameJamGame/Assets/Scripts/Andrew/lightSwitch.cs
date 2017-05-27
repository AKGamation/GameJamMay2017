using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitch : MonoBehaviour
{
    public Light lightToChange;
    private bool lightOn = true;

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
        lightOn = !lightOn;
        lightToChange.intensity = (lightOn ? 0 : 1);
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
