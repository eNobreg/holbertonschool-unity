using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertAxis;
    // Update is called once per frame

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (PlayerPrefs.GetInt("isInverted") == 1)
            invertAxis.isOn = true;
        else
            invertAxis.isOn = false;
    }
    public void Back()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerPrefs.GetString("lastScene")));
        PauseMenu.canvasShared.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("Options");
    }

    public void Apply()
    {
        if (invertAxis.isOn)
            PlayerPrefs.SetInt("isInverted", 1);
        else
            PlayerPrefs.SetInt("isInverted", 0);
        
        Back();
    }
}
