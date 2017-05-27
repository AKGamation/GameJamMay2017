using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this will eventually make a bunch of stars
public class stars : MonoBehaviour
{
    public int numberOfStars = 1000; // number of stars to spawn
    public float distanceFromOrigin = 100.0f; // distance from the origin
    public GameObject starObject; // star model
    public List<Color> colorRange = new List<Color>();

	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < numberOfStars; i++)
        {
            Vector3 newLocation = Random.onUnitSphere * distanceFromOrigin;
            Vector3 newRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject currentStar = Instantiate(starObject, newLocation, Quaternion.identity) as GameObject;
            currentStar.GetComponent<MeshRenderer>().material.color = Color.Lerp(colorRange[0], colorRange[1], Random.Range(0.0f, 1.0f));
        }
	}
}
