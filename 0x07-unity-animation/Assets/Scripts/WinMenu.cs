using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex != 4) // If it is not the last level
            SceneManager.LoadScene(buildIndex + 1);
        else //Load MainMenu
            MainMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
