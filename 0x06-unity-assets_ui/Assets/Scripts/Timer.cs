using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
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

    public void Finish()
    {
        finished = true;
        timerText.color = Color.green;
        timerText.fontSize = 60;
        this.GetComponent<Timer>().enabled = false;
    }
}
