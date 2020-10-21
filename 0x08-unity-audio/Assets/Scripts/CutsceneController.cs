using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public GameObject cam;
    public GameObject camBase;
    public GameObject controller;
    public GameObject timer;
    public GameObject cutCam;
    // Start is called before the first frame update
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level01")
        {
            this.gameObject.GetComponent<Animator>().SetBool("level1", true);
        }
        if (SceneManager.GetActiveScene().name == "Level02")
        {
            this.gameObject.GetComponent<Animator>().SetBool("level2", true);
        }
        if (SceneManager.GetActiveScene().name == "Level03")
        {
            this.gameObject.GetComponent<Animator>().SetBool("level3", true);
        }
    }
    public void enableMovement(float finished)
    {
        if (finished == 1)
        {
            camBase.gameObject.GetComponent<CameraControllerBase>().animationPlaying = false;
            cam.gameObject.SetActive(true);
            controller.gameObject.GetComponent<PlayerController>().enabled = true;
            timer.gameObject.SetActive(true);
            cutCam.gameObject.SetActive(false);
        }
    }


}
