using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cam;
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Reached");
            //cam.gameObject.GetComponent<CameraMove>().enabled = true;
            LevelGeneration.doGeneration = true;
        }
    }
}
