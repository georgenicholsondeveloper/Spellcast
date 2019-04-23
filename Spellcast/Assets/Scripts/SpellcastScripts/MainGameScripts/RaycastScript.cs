using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour {

    public GameObject origin;
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        Raycast();
	}

    void Raycast()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Physics.Raycast(origin.transform.position, Vector3.forward);
        Debug.DrawRay(origin.transform.position, origin.transform.forward, Color.green);

    }
}
