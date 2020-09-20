using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("u");
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escape");
            if (isPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
