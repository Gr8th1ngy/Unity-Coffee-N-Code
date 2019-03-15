using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroybyContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion; 

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Boundary")
        {
            return;
        }

        if (explosion != null) 
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player" && playerExplosion != null)
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
