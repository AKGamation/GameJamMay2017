using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveItem : MonoBehaviour
{
    private bool pickedUp = false;
    private Rigidbody myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    public void onPress()
    {
        pickedUp = !pickedUp;
    }

    public void onHold()
    {
        
    }

    public void onRelease()
    {
        myRigidBody.isKinematic = false;
    }

    void Update()
    {
        if(pickedUp)
        {
            transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
