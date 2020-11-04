using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AnimationHandler : MonoBehaviour
{
	public Animator gitHub;
	private void Start()
	{
		gitHub = GetComponentInChildren<Animator>();
	}

	public void playAnimations()
	{
		Debug.Log("Tracked");
		gitHub.SetTrigger("playAnimations");
	}

	public void stopAnimations()
	{

	}
}
