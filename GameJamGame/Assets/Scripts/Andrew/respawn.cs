using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{

    // store start position
    private Vector3 startPosition;

    // called once at the start of the game
    void Start ()
    {
        startPosition = transform.position;
    }

    // called when this object enters a respawn trigger
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "respawn")
        {
            if(GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            transform.position = startPosition;
            Debug.Log("respawning" + this.gameObject.name);
        }
    }
}
