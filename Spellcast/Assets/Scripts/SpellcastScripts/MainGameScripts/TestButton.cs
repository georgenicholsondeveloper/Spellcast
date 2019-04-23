using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour {

    public GameObject  castPoint, castSphere;

    private GameObject LeftHand, head;
    private GameObject spawneee;
    private Vector3 rCenter = Vector3.zero;
    private float sphereRadius;


	void Awake () {
        LeftHand = GameObject.Find("Controller (left)");
        head = GameObject.Find("Camera (eye)");
    }

	void Update () {
        if (Input.GetButtonDown("Fire1")) //b
        {
            if (spawneee) { Destroy(spawneee); }
            spawneee = Instantiate(castSphere);
            spawneee.GetComponent<SphereCollider>().radius = sphereRadius;
            spawneee.transform.position = rCenter;
            print(spawneee.transform.position);
            spawneee.transform.Translate(new Vector3(0, -.1f, 0), Space.World);
            print(spawneee.transform.position);
            spawneee.transform.SetParent(head.transform); 
        }

        if (Input.GetButtonDown("Fire3")) //y
        {
            print("Left");
            CalculateRadiusOfSphere(LeftHand.transform.position, castPoint.transform.position);
            rCenter = LeftHand.transform.position;
        }

        if (Input.GetButtonDown("Fire2")) //a
        {
            //Attempt
        }
        if (Input.GetButtonDown("Jump")) //x
        {
            //RESET
        }
	}

    private void CalculateRadiusOfSphere(Vector3 Center, Vector3 passPoint)
    {
        float rX = passPoint.x - Center.x;
        print(rX);
        rX *= rX;
        print(rX);
        float rY = passPoint.y - Center.y;
        rY *= rY;
        float rZ = passPoint.z - Center.z;
        rZ *= rZ;
        float r2 = rX + rY + rZ;
        sphereRadius = Mathf.Sqrt(r2);
        print(sphereRadius);
        //sphereRadius = sphereRadius - sphereRadius / 200;
        print(r2);
        print(sphereRadius);

        //The formula for the equation of a sphere
        //https://www.kristakingmath.com/blog/center-radius-and-equation-of-the-sphere
    }
}
