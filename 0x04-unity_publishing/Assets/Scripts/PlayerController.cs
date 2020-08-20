using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float speed = 10;
    public float boostSpeed = 2;
    float moveHorizontal;
    float moveVertical;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
	public Image resultImage;
	public Text resultText;
    public HealthBar healthBar;
    void SetScoreText()
    {
            scoreText.text = "Score: " + score.ToString();
    }
    void Start()
    {
        health = 5;
        rb = GetComponent<Rigidbody>();
        SetScoreText();
        healthBar.SetMaxHealth(health);
        
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis ("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("menu");
		}
        if (health == 0)
        {
			resultImage.gameObject.SetActive(true);
			resultImage.color = Color.red;
			resultText.color = Color.white;
			resultText.text = "Game Over!";
            health = 5;
            score = 0;
			StartCoroutine(LoadScene(3));
        }
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(move * speed);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            health -= 1;
            healthBar.SetHealth(health);
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            resultImage.gameObject.SetActive(true);
			resultImage.color = Color.green;
			resultText.color = Color.black;
			resultText.text = "You Win!";
			StartCoroutine(LoadScene(2));
        }
        if (other.gameObject.CompareTag("Boost"))
        {
            Vector3 move = new Vector3 (moveHorizontal, 80.0f, moveVertical);
            rb.AddForce(move * speed);
        }
    }

	IEnumerator LoadScene(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene("maze");
	}
}
