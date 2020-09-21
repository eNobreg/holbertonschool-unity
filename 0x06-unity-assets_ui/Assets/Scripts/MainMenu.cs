using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void Options()
    {
        PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
