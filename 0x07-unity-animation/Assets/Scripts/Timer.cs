using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text finalText;
    public GameObject winScreen;
    private float startTime;
    private bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            float time = Time.time - startTime;

            string minutes = ((int) time / 60).ToString();
            string seconds = (time % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;
        }
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finished = true;
        finalText.text = timerText.text;
        winScreen.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
        this.GetComponent<Timer>().enabled = false;
    }
}
