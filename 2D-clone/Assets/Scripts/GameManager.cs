using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public deathMenu deathScreen;
    public Score scoreDisplay;

    public float transitionTime = 1f;
    public Animator transition;

    // Update is called once per frame;

    public void restartGame()
    {
        player.gameObject.SetActive(false);
        cam.gameObject.GetComponent<CameraMove>().enabled = false;
        deathScreen.gameObject.SetActive(true);
        scoreDisplay.gameObject.SetActive(false);
        LevelGeneration.doGeneration = false;
    }

    public void reset()
    {
        string scene = SceneManager.GetActiveScene().name;
        StartCoroutine(loadLevel(scene));
    }

    public void quitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator loadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
