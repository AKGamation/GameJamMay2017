using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonColor : MonoBehaviour
{
    private Material material;

    public Color offColor;
    public Color hoverColor;
    public Color onColor;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // called while the button is not being interacted with
    public void onIdle()
    {
        material.color = offColor;
    }

    // called while the button is being hovered
    public void onHover()
    {
        material.color = hoverColor;
    }

    // called once when the button is first pressed
    public void onPress()
    {

    }

    // called when the button is held
    public void onHold()
    {
        material.color = onColor;
    }

    // called once when the button stops being pressed
    public void onRelease()
    {

    }
}
