using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject canvasesScene;
    public static GameObject canvasShared;
    public static bool isPaused = false;
    public GameObject pauseUI;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unPaused;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
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
        paused.TransitionTo(.001f);
        pauseUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        unPaused.TransitionTo(.001f);
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        canvasShared = canvasesScene;
        PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        canvasesScene.gameObject.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Options"));
    }
}
