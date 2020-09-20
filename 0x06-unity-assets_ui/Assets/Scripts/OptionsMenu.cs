using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    // Update is called once per frame
    public void Back()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerPrefs.GetString("lastScene")));
        SceneManager.UnloadSceneAsync("Options");
    }
}
