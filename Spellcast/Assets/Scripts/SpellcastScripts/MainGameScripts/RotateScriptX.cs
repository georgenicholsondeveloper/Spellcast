using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScriptX : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(15, 0, 0 * Time.deltaTime);
	}
}
