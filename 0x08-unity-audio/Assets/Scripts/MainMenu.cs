using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer masterMixer;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        masterMixer.SetFloat("bgmVolume", PlayerPrefs.GetFloat("volume"));
    }
    public void LevelSelect(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }
    /*public void Options()
    {
        canvasShared = canvasesScene;
        PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        canvasesScene.gameObject.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Options"));
    }*/

    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
