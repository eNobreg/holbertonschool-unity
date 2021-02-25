using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.AI;
using System;

public class PlaneSelection : MonoBehaviour
{
	public NavMeshSurface nav;
	public TargetSpawner targetManager;
    // Start is called before the first frame update
	private ARRaycastManager rayManager;
	public ARPlaneManager planeManager;
	private Touch touch;
	List<ARRaycastHit> rayHits = new List<ARRaycastHit>();

	private bool deacivatedPlanes = false;

	public bool test;

	public GameObject startButton;
	public GameObject selectedPlane = null;
    void Start()
    {
		nav = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
        rayManager = GetComponent<ARRaycastManager>();
		planeManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
		{
			if (selectedPlane == null && rayManager.Raycast(Input.GetTouch(0).position, rayHits))
			{
				selectedPlane = handleHits(rayHits[0]);
				selectedPlane.tag = "SelectedPlane";
				targetManager.selectedPlane = selectedPlane;
				planeManager.enabled = false;

				// Added for when reset is run
				deacivatedPlanes = false;
			}
		}

		if (selectedPlane != null && deacivatedPlanes == false)
		{
			foreach (var plane in planeManager.trackables)
			{
				if (plane.gameObject.GetComponent<PlaneValues>().isSelection == false)
				{
					plane.gameObject.SetActive(false);
				}
			}
			deacivatedPlanes = true;

			nav.BuildNavMesh();
			if (nav.navMeshData != null && NavMesh.CalculateTriangulation().areas.Length > 0)
			{
				Debug.Log("Baked");
			}
			targetManager.SendMessage("SpawnTargets");
			startButton.SetActive(true);
			this.gameObject.GetComponent<PlaneSelection>().enabled = false;
		}
    }
	
	public GameObject handleHits(ARRaycastHit hit)
	{
		if ((hit.hitType & TrackableType.Planes) != 0)
		{
			deacivatedPlanes = false;
			var plane = planeManager.GetPlane(hit.trackableId);
			Debug.Log(plane.transform.gameObject.GetComponent<PlaneValues>().isSelection);
			
			plane.transform.gameObject.GetComponent<PlaneValues>().isSelection = true;
			Debug.Log(plane.transform.gameObject.GetComponent<PlaneValues>().isSelection);
			return(plane.transform.gameObject);
		}
		return (null);
	}
}
