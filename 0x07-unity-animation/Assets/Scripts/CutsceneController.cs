using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject cam;
    public GameObject controller;
    public GameObject timer;
    public GameObject cutCam;
    // Start is called before the first frame update
    public void enableMovement(float finished)
    {
        if (finished == 1)
        {
            cam.gameObject.SetActive(true);
            controller.gameObject.GetComponent<PlayerController>().enabled = true;
            timer.gameObject.SetActive(true);
            cutCam.gameObject.SetActive(false);
        }
    }


}
