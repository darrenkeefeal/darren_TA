using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont_Destroy_Event : MonoBehaviour {

	public void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1) 
		{
			Destroy (gameObject);
		}
	}
}
