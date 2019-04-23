using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
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
            health -= 15;
        }

        if(collision.gameObject.tag == "Lightning")
        {
            Destroy(collision.gameObject);
            health -= 10;
        }
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
            manager.GetComponent<MatchVictorScript>().PlayerLose(this.gameObject);
    }
}
