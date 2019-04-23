using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Force : NetworkBehaviour {

    public int Bouncecount = 0;
    public GameObject player;
    private Rigidbody RB;
    float xForce = 0;
    bool Positive = false;
    public float minusBy = 5, Limit = 25;


    private void Awake()
    {
        RB = gameObject.GetComponent<Rigidbody>();

        GameObject [] playersInScene = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject i in playersInScene)
        {
            if (i.transform.parent == isLocalPlayer)
            {
                player = i;
            }
        }
    }

    void Update () {
        if (Bouncecount >= 3)
        {
            player.GetComponent<SpellSystem>().OnceInstance = false;
            Destroy(gameObject);  
        }
        if (!Positive)
        {
            xForce -= minusBy;
            if (xForce <= -Limit)
            {
                Positive = true;
            }
        }
        else
        {
            xForce += minusBy;
            if (xForce >= Limit)
            {
                Positive = false;
            }
        }
        RB.AddForce(Vector3.right * xForce);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "floor")
        {
            print(Bouncecount);
            Bouncecount++; 
        }
    }

}
