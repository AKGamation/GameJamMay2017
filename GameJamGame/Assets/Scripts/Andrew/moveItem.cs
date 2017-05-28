using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveItem : MonoBehaviour
{
    private bool pickedUp = false;
    private Collider myCollider;
    public GameObject relatedScanner;

    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

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
        pickedUp = !pickedUp;
        myCollider.isTrigger = (pickedUp ? true : false);
    }

    // called when the button is held
    public void onHold()
    {
        
    }

    // called once when the button stops being pressed
    public void onRelease()
    {

    }

    // called every frame
    void Update()
    {
        if(pickedUp)
        {
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == relatedScanner)
        {
            other.gameObject.GetComponent<DoorButton>().toggleMulti();
            Destroy(this.gameObject);
        }
    }
}
