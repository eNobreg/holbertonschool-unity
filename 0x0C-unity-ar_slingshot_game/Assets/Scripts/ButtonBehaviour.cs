using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
	public GameManager manager;
	
	public PlaneSelection selector;
	public TargetSpawner spawner;
	public GameObject startButton;

	public GameObject restartButtonObj;

	public GameObject GameButtons;
	public GameObject searchText;

	public GameObject GameUI;
    void Start()
    {
		manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void startGame()
	{
		foreach (GameObject target in spawner.targets)
		{
			target.GetComponent<WanderingAI>().enabled = true;
			
		}

		spawner.SpawnAmmo();
		GameUI.SetActive(true);
		GameButtons.SetActive(true);
		spawner.gameStarted = true;
		searchText.SetActive(false);
		startButton.SetActive(false);
	}

	// Restart current game on the same plane
	// Call spawn targets
	// Reset ammo count
	public void restartButton()
	{
		spawner.ammoCount = 7;
		Debug.Log(spawner.targets.Count);
		for (int i = 0; i < spawner.targets.Count; i++)
		{
			spawner.targets[i].SetActive(false);
			spawner.targets[i].gameObject.SetActive(false);
			GameObject.Destroy(spawner.targets[i]);
		}
		spawner.targets.Clear();
		spawner.SpawnTargets();
		
		foreach (GameObject target in spawner.targets)
		{
			target.GetComponent<WanderingAI>().enabled = true;
			
		}
		manager.score = 0;
		restartButtonObj.SetActive(false);
	}

	public void resetButton()
	{
		selector.enabled = true;
		selector.selectedPlane.tag = "Plane";
		selector.selectedPlane.SetActive(false);
		selector.selectedPlane = null;

		GameObject.Destroy(spawner.spawnedAmmo);

		selector.planeManager.enabled = true;

		manager.score = 0;
		spawner.ammoCount = 7;
		spawner.ammoText.text = 7.ToString();
	}

	public void quitButton()
	{
		Application.Quit();
	}
}
