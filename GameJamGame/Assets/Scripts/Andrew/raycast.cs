using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public float playerReach; // how far the player can reach
    Ray ray;
    RaycastHit hitInfo;
    private int layerMask = 1<<8;
	
	// Update is called once per frame
	void Update ()
    {
        // make ray
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction, Color.cyan);
        // raycast
        if (Physics.Raycast(ray, out hitInfo, playerReach, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("hit something");
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("player has clicked on " + hitInfo.collider.gameObject.name);
            }
        }
	}
}
