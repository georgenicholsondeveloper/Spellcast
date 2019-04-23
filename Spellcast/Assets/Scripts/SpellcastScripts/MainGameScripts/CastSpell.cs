using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CastSpell : MonoBehaviour {

    public GameObject player;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CastRadius"))
        {
            if (player.GetComponent<SpellSystem>().castCounter != 0)
                player.GetComponent<SpellSystem>().shouldCast = true;
        }
    }
}
