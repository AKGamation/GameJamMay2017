using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public float playerReach; // how far the player can reach
    Ray ray;
    RaycastHit hitInfo;
    private int layerMask = (LayerMask.NameToLayer("interactive"));
	
	// Update is called once per frame
	void Update ()
    {
        // make ray
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // raycast
        if(Physics.Raycast(ray, out hitInfo, playerReach, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (Input)
        }
	}
}
