using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public Rigidbody rigidbodyToSpin;
    float spinSpeed = 0.0f;
    public float maxSpinSpeed;
    public float force;

    // called once when the button is first pressed
    public void onPress()
    {
        if(spinSpeed < maxSpinSpeed)
        {
            spinSpeed += force;
        }
    }

    // called in physics step
    void FixedUpdate()
    {
        rigidbodyToSpin.AddTorque(new Vector3(0, spinSpeed, 0), ForceMode.Force);
    }
}
