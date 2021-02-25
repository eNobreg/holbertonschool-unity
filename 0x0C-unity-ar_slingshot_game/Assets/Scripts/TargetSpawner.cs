using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TargetSpawner : MonoBehaviour
{
	public float dist = 20.0f;
	public NavMeshSurface navMesh;
	public int targetAmount = 5;

	public bool ammoLaunched;

	public bool gameStarted;
    // Start is called before the first frame update
	public GameObject target;
	public GameObject ammo;

	public List<GameObject> targets;

	public GameObject selectedPlane;
	public GameObject arcRenderer;

	public GameObject spawnedAmmo;

	public Text ammoText;
	public int ammoCount = 7;

	private Vector3 startPos;

	public GameObject restartButton;



	private void Start() {
		ammoLaunched = false;
		ammoText.text = ammoCount.ToString();
		Vector3 FirstStartPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0.5f));
	}

	private void Update() 
	{
		if (ammoLaunched == true && ammoCount > 0)
		{
			SpawnAmmo();
			ammoText.text = ammoCount.ToString();
		}

		if (gameStarted && targets.Count == 0 || (ammoCount == 0 && ammoLaunched == true))
		{
			ammoText.text = ammoCount.ToString();
			restartButton.SetActive(true);
		}
	}

	public void SpawnAmmo()
	{
		Debug.Log("Spawned");
		Vector3 screenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.5f);
		startPos = Camera.main.ScreenToWorldPoint(screenPos);
		spawnedAmmo = GameObject.Instantiate(ammo, startPos, Quaternion.identity);
		spawnedAmmo.GetComponent<AmmoBehaviour>().plane = selectedPlane;
		ammoLaunched = false;
	}
	public void SpawnTargets()
	{
		GameObject plane = GameObject.FindGameObjectWithTag("SelectedPlane");
		Debug.Log(plane);
		Debug.Log("Spawn Targets");
		for (int i = 0; i < targetAmount; i++)
		{
			Debug.Log("Entered Loop");
			Vector3 origin = plane.GetComponent<MeshRenderer>().bounds.center;
			float adjustedDist = dist - 1;
			NavMeshHit navHit;

			Vector3 randDirection = Random.insideUnitSphere * adjustedDist;
 
        	randDirection += origin;
 
        	NavMesh.SamplePosition (randDirection, out navHit, dist * 2, -1);
 
        	GameObject obj = Instantiate(target, transform.position, Quaternion.identity);
			bool warped = obj.GetComponent<NavMeshAgent>().Warp(navHit.position);  
			targets.Add(obj);

			if (warped)
				Debug.Log("Spawned Target");
		}
    }
}
