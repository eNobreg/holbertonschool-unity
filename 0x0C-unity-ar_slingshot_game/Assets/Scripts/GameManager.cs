using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
	public Text scoreText;
	public GameObject scoreObject;
	public int score = 0;
	private int currentScore = 0;




    // Update is called once per frame
    void Update()
    {
		Debug.Log(score);
		if (score == 0)
			currentScore = 0;

		if (score > currentScore)
		{
			currentScore = score;
			scoreText.text = currentScore.ToString();
		}
    }

}
