﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastRotateX : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(800, 0, 0 * Time.deltaTime);
	}
}