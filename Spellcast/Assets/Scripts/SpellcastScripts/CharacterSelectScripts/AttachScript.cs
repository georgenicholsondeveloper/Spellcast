using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachScript : MonoBehaviour {
    public GameObject hand;
    // Use this for initialization
    void Start () {
        transform.parent.position = hand.transform.position + new Vector3(0, -0.05f, 0);
        transform.parent.eulerAngles = hand.transform.eulerAngles - new Vector3(-129, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        transform.parent = hand.transform;
	}

}
