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
            player.GetComponent<SpellSystem>().shouldCast = true;
        }
    }
}
