using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathMenu : MonoBehaviour
{
    public void RestartGame()
    {
        FindObjectOfType<GameManager>().reset();
    }

    public void quitToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
