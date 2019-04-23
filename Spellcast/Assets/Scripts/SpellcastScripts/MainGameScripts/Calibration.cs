using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CastRadius"))
        {
            print("in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CastRadius"))
        {
            print("out");
        }
    }
}
