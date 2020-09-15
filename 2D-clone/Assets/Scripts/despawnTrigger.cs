using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnTrigger : MonoBehaviour
{   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sweep"))
        {
            Debug.Log("Delete");
            Destroy(this.gameObject, 3f);
        }
    }
}
