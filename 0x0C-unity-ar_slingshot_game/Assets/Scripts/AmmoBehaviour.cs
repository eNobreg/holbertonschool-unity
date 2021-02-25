using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
	private Touch touch;
	public GameManager manager;
	public GameObject plane;

	public LaunchArcRenderer arcRenderer;

	public TargetSpawner spawner;

	public float launchThrust;
	private Vector3 startPos;

	private bool isPressed = false;
	public float maxDistance = 1f;

	private Rigidbody rb;
	public float basePower = 25.0f;
	public float speedMod = 1f;
   private void Awake()
    {
		 manager = GameObject.Find("GameManager").GetComponent<GameManager>();
         startPos = this.transform.position;
		 rb = GetComponent<Rigidbody>();
		 spawner = GameObject.Find("TargetSpawner").GetComponent<TargetSpawner>();
		 rb.useGravity = false;
		 arcRenderer = GameObject.Find("arcStart").GetComponentInChildren<LaunchArcRenderer>();
		 Debug.Log(arcRenderer);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
     		if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) 
     		{
         		// get the touch position from the screen touch to world point
         		Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.3f));

				if (0 == 0)
				{
					isPressed = true;
         			// lerp and set the position of the current object to that of the touch, but smoothly over time.
					//if (Vector3.Distance(startPos, touchedPos) > maxDistance)
					//{
						//Vector3 fixedPos = startPos + (touchedPos - startPos).normalized * maxDistance;
         				//transform.position = Vector3.Lerp(transform.position, fixedPos, Time.deltaTime * 4);
					//}
					//else
					//{
					transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * 4);
					//}
					launchThrust = Vector3.Distance(startPos, transform.position) * basePower;
					arcRenderer.velocity = launchThrust;
					arcRenderer.angle = Vector3.Angle(startPos, transform.position);
				}
     		}
		}
		else
		{
			if (isPressed == true)
			{
				isPressed = false;
				LaunchAmmo();
			}
		}
    }
	private void LaunchAmmo()
	{
		//arcRenderer.gameObject.SetActive(false);
		Debug.Log("Launched");
		rb.useGravity = true;
		rb.AddForce((startPos - transform.position) * launchThrust);
		arcRenderer.gameObject.GetComponent<LineRenderer>().enabled = false;
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Hit a collider");
		if (other.gameObject.CompareTag("Target") || other.gameObject.CompareTag("SelectedPlane") || other.gameObject.CompareTag("BelowPlane"))
		{
			if (other.gameObject.CompareTag("Target"))
			{
				spawner.targets.Remove(other.gameObject);
				other.gameObject.SetActive(false);
				manager.score += 10;
			}
			spawner.ammoLaunched = true;
			spawner.ammoCount -= 1;
			this.gameObject.SetActive(false);
		}
	}
}
