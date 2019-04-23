using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour {

    public void Pressed()
    {
        print("Hello");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameObject.GetComponentInChildren<Image>().color = Color.blue;
            if (Input.anyKeyDown)
            {
                Pressed();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Image>().color = Color.white;

        }
    }
}
