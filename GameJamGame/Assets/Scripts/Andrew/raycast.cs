using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public float playerReach; // how far the player can reach
    Ray ray;
    public RaycastHit hitInfo;
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
            hitInfo.collider.gameObject.GetComponent<buttonStateManager>().state = buttonStateManager.buttonState.HOVER;
            if (Input.GetMouseButton(0))
            {
                hitInfo.collider.gameObject.GetComponent<buttonStateManager>().state = buttonStateManager.buttonState.HOLD;
            }
            if(Input.GetMouseButtonDown(0))
            {
                hitInfo.collider.gameObject.GetComponent<buttonStateManager>().state = buttonStateManager.buttonState.PRESS;
            }
            if(Input.GetMouseButtonUp(0))
            {
                hitInfo.collider.gameObject.GetComponent<buttonStateManager>().state = buttonStateManager.buttonState.RELEASE;
            }
        }
	}
}
