using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{

	public void toURL(string url)
	{
		Application.OpenURL(url);
	}
}
