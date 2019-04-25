using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamageScript : NetworkBehaviour
{
    public int health = 100;
    GameObject manager;

    private void Start()
    {
        manager = GameObject.Find("Manager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            Destroy(collision.gameObject);
            if (!gameObject.GetComponent<PlayerCharacterScript>().isReady)
                health -= 15;
        }

        if (collision.gameObject.tag == "Lightning")
        {
            Destroy(collision.gameObject);
            if (!gameObject.GetComponent<PlayerCharacterScript>().isReady)
                health -= 10;
        }

        if (health <= 0)
        {
            GameObject i = this.gameObject;
            CmdSendDeath(i);
        }
    }

    [Command]
    public void CmdSendDeath(GameObject i)
    {
        RpcSendDeath(i);
    }

    [ClientRpc]
    public void RpcSendDeath(GameObject i)
    {
        manager.GetComponent<MatchVictorScript>().PlayerLose(i);
    }
}
