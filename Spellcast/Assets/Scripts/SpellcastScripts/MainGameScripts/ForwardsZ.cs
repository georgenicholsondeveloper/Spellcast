using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardsZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = GetComponent<Rigidbody>().velocity;
	}
}
