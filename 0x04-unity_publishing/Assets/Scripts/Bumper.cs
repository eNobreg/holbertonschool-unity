using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{    
    public int bumperForce = 800;

    public GameObject player;
     
    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered");
            player.GetComponent<Rigidbody>().AddExplosionForce(bumperForce, transform.position, 20f, 0);
        }
    }
}
